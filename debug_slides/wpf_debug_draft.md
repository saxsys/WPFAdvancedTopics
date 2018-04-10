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

## Live Visual Tree / Live Property Explorer

Feature seit Visual Studio 2015 um direkt aus Visual Studio heraus den Visual Tree zu untersuchen und auch zu manipulieren.

## IValueConverter

In den meisten Fällen lässt ich ein IValueConverter einsetzen gegen Eigenschaften die man im XAML Code bindet und in diesem C# Code kann man wie gewohnt Breakpoints setzen und debuggen.

**TODO:** *Beispiel in Janus einbauen und beschreiben*

## PresentationTraceSources

Detailierte Debug Ausgaben in der Konsole bzw. mit etwas umweg debugging.

Im Codebehind der App.xaml.cs die OnStartup-Methode überschreiben und PresentationTraceSources dort initialisieren.

    protected override void OnStartup(StartupEventArgs e)
    {
        PresentationTraceSources.Refresh();
        PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());
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
	
Beim binding im XAML Code für das zu untersuchende Property die TraceLevel setzen.
	
	Text="{Binding Output, PresentationTraceSources.TraceLevel=High}"
	
**TODO:** *mehr Informationen und Möglichkeiten beschreiben?*