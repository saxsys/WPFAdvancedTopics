# WPF Debugging

## Abstract

Sammlung von unterschiedlichen Möglichkeiten mit dem man Fehler / Unerwartetes Aussehen in WPF Anwendungen nachvollziehen kann.

## Snoop

Kleine Anwendung mit der sich der Visual Tree von einer laufenden WPF Anwendung untersuchen und manipulieren lässt.

Allgemeiner Workflow:

1. Gewünschte WPF Anwendung starten
2. WPF Snoop starten
3. Anwendung mittels Snoop "ins Visier" nehmen
4. Den Visual Tree untersuchen / ein Element suchen
5. Im Tab "Property" sind die verfügbaren Properties anzeigbar sowie manipulierbar. Beispiel: IsEnabled per Checkbox ein-/ausschalten oder Background auf einen gewünschten Wert setzen (per Name oder Hex-Code)

Man sieht den Visual Tree wie er gerade zur Laufzeit aufgebaut ist. 

Er spiegelt nicht den Aufbau im Projekt durch ein oder mehr Quellcode Dateien wieder.

## WPF Tree Visualizer

Feature seit Visual Studio 2015 um direkt aus Visual Studio heraus den Visual Tree zu untersuchen und auch zu manipulieren.

Aufgeteilt in zwei Komponenten:
1. Live Visual Tree 
2. Live Property Explorer

### Live Visual Tree

Zeigt analog zu Snoop den aktuellen Visual Tree an und bietet zusätzlich soweit verfügbar einen Verknüpfung zu der entsprechenden stelle im Code an.

### Live Property Explorer

Hier werden die Properties zu einem aus dem Visual Tree ausgewählten Eintrag angezeigt plus ein paar zusätzliche Funktionen:
* Je nach Binding kann der Wert live geändert werden
* Wenn es sich um Styling Informationen handelt, gibt es eine Verknüpfung zu dem jeweiligen Code.
* Es sind Vererbungshierarchien erkennbar

## IValueConverter

Normalerweise werden Converter eingesetzt um eine Umwandlung von Informationen in der UI ermöglichen.

Beispiel: Die Zahlen in der Taschenrechner-App sollen grün oder rot angezeigt werden, je nachdem ob der Wert positiv oder negativ ist.

Auf diese weise kann man in dem Converter Code auch Breakpoints setzen und damit indirekt das binding debuggen.

Für den einfachsten Fall kann man in beiden Fällen den zu konvertierenden Wert direkt mit return zurück geben und dort ein Breakpoint setzen.

## PresentationTraceSources

Detailierte Debug Möglichkeit von Bindings.

Im Codebehind der App.xaml.cs die OnStartup-Methode überschreiben und PresentationTraceSources dort initialisieren.

    protected override void OnStartup(StartupEventArgs e)
    {
        PresentationTraceSources.Refresh();
        PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
		PresentationTraceSources.DataBindingSource.Listeners.Add(new TextWriterTraceListener(Path.GetTempFileName()));
        PresentationTraceSources.DataBindingSource.Listeners.Add(new DebugTraceListener());
        PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Warning | SourceLevels.Error;
        base.OnStartup(e);
    }
	
	public class DebugTraceListener : TraceListener
    {
        public override void Write(string message)
        {
        }
    
        public override void WriteLine(string message)
        {
            Debugger.Break();
        }
    }

Es können ein oder mehr Listeners definiert werden, welche die weitere Verarbeitung der Debug-Informationen steuern.

Im Beispiel sind es drei stück:
1. ConsoleTraceListener: Ausgabe der Informationen über die Konsole
2. TextWriterTraceListener: Ausgabe der Informationen in eine Datei oder Stream-Objekt
3. DebugTraceListener: Einfaches Beispiel für eine eigene Implementierung von `TraceListener` mit der bei jeder Ausgabe der Debugger unterbrochen wird.

Beim Binding im XAML Code kann dann die TraceLevel für jedes binding eigens eingestellt werden.
	
	Text="{Binding Output, PresentationTraceSources.TraceLevel=High}"
	
Fun-Fact: Das Overlay mit dem der *Live Visual Tree* im jeweiligen aktiven Fenster gesteuert wird, taucht hier auch auf.