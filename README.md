# Technische Dokumentation
Dieses Dokument beinhaltet die technische Dokumentation des Projektes "AR Ranger" von Jona König und David Credo. Es soll einen Überblick über die Systeme und Komponenten des Projektes geben und deren Funktionsweisen erläutern.

Projekttitel: AR Ranger
Unity Version: 2022.3.17f1
Verwendete AR/VR Hardware: Open XR, Meta Quest 3 
Teammitglieder: Jona König, David Credo

## Inhaltsverzeichnis

1. [Einleitung](#einleitung)
    - [Ordnerstruktur](#ordnerstruktur)
2. [Grundlegende Architektur](#grundlegende-architektur)
    - [Event-Bus](#event-bus)
    - [Dependency Injection](#dependency-injection)
    - [Scriptable Objects](#scriptable-objects)
3. [Eigenleistungen und 3rd-Party-Assets](#eigenleistungen-und-3rd-party-assets)
    - [Eigenleistungen](#eigenleistungen)
    - [3rd-Party-Assets](#3rd-party-assets)
4. [Teilsysteme und Komponenten (UML)](#teilsysteme-und-komponenten-uml)
    - [High Level State Diagram des Spiels](#high-level-state-diagram-des-spiels)
    - [UML-Diagramm des Onboarding-Systems](#uml-diagramm-des-onboarding-systems)
    - [State Diagramm des Scanner-Systems](#state-diagramm-des-scanner-systems)

## Einleitung

Das gesamte Projekt ist mit C# DocStrings dokumentiert, sodass die Funktionsweise der einzelnen Klassen und Methoden im Code nachvollzogen werden kann. Die technische Dokumentation soll einen Überblick über die Architektur und die Funktionsweise des Projektes geben.

### Ordnerstruktur
Der Großteil der Ressourcen befindet sich im `Assets`-Ordner. Dieser ist in mehrere Unterordner unterteilt, die die verschiedenen Arten von Ressourcen enthalten. 
Die meisten Ordner sind selbsterklärend, für das technische Verständnis ist der Aufbau des `Scripts`-Ordners jedoch von Bedeutung. Dieser ist in mehrere Unterordner unterteilt, die die verschiedenen Features des Projektes enthalten.
- `_Scripts`: Enthält alle Skripte, die im Projekt verwendet werden. Die Skripte sind nach Feature (zB. Onboarding, Scanner, etc.) sortiert.
- `_Scripts/General`: Enthält grundlegende Systeme wie ein leichtgewichtiges Dependency Injection System, das für die Entkopplung von Komponenten verwendet wird. Hier befindet sich ebenfalls ein Event-Bus System, das für die Kommunikation zwischen Komponenten verwendet wird.

## Grundlegende Architektur

### Event-Bus

![Event-Bus](./Documentation/Images/High%20_Level_Overview.png)

Das Event-Bus System stellt das zentrale Kommunikationsmittel zwischen den verschiedenen Komponenten des Projektes dar. Es ermöglicht das Senden und Empfangen von Nachrichten, ohne dass die Komponenten voneinander wissen müssen. Dadurch wird eine starke Entkopplung der Komponenten erreicht, was die Wartbarkeit und Erweiterbarkeit des Projektes erhöht.
Beispielhafte Verwendung:

```csharp
// Senden einer Nachricht, am Beispiel eines Scan-Events des Scanners den der Spieler nutzen kann

EventBus<ScanEvent>.Raise(new ScanEvent());

//Registrierung eines Empfängers, der auf das Scan-Event reagiert soll:

void OnEnable()
{
    EventBus<ScanEvent>.Register(OnScan);
}

void OnDisable()
{
    EventBus<ScanEvent>.Unregister(OnScan);
}

void OnScan(ScanEvent e)
{
    // Reaktion auf das Scan-Event
}
```

- Wie im Beispiel zu sehen, erwartet der EventBus ein EventBinding, welches eine Methode mit einem Parameter des Event-Typs erwartet.
- Der EventBus ist als statische Klasse realisiert, welche mittels Reflection beim Start des Spiels alle Event-Typen in den Assemblies des Projektes sucht und registriert. Dadurch müssen keine Event-Channels manuell initialisiert werden.
- **Wichtig**: Eigene Assemblies müssen in der Datei `PredefinedAssemblyUtil.cs` hinzugefügt werden, damit der EventBus auch Events aus diesen Assemblies registriert.
- Der EventBus ist generisch, sodass für jeden Event-Typ ein eigener EventBus existiert. Dadurch wird sichergestellt, dass nur Events des richtigen Typs gesendet und empfangen werden können.

### Dependency Injection

- Im Projekt wird vereinzelt Dependency Injection verwendet, um die Abhängigkeiten zwischen den verschiedenen Komponenten zu entkoppeln. Um eine Dependency bereitzustellen, muss man ein MonoBehaviour mit einer Methode erstellen, welche das `Provide`-Attribut trägt. Diese Methode wird dann beim Start des Spiels aufgerufen und die bereitgestellte Abhängigkeit wird von einem Injector in die entsprechenden Felder der abhängigen Komponenten injiziert. 
- Abhängige Komponenten müssen das `Inject`-Attribut tragen, um die Abhängigkeit injiziert zu bekommen. Dieses Attribut kann auf Feldern, Properties und Methoden verwendet werden.
- Der Injection ist ein Singleton MonoBehaviour, das die Abhängigkeiten verwaltet und die Injektion durchführt. Es muss in jeder Szene vorhanden sein, in der Dependency Injection verwendet wird.

### Scriptable Objects

- Scriptable Objects stellen einen zenralen Bestandteil der Architektur dar. Sie werden verwendet, um Konfigurationen, Daten und Zustände zu speichern und zu verwalten. 
- Beispielsweise werden die Daten über Lebewesen in Scriptable Objects gespeichert, um sie einfach zu konfigurieren und zu verwalten, sowie deren Zustand (ob der User diese bereits gescannt hat) zu speichern. Durch die globale Zugänglichkeit von Scriptable Objects können sie einfach von verschiedenen Komponenten verwendet werden.
- Scriptable Objects werden auch verwendet, um die Konfiguration von Features zu ermöglichen. Beispielsweise werden die Konfigurationen des Onboarding-Systems in Scriptable Objects gespeichert, um sie einfach zu konfigurieren und zu verwalten. 

## Eigenleistungen und 3rd-Party-Assets

### Eigenleistungen

1. Audio-Dateien

- Die Voice-Over-Aufnahmen wurden von uns selbst erstellt und bearbeitet.

2. 3D-Modelle

- Scanner
- Sanderling
- Salzmiere
- Sepia/Schulp 
- Leuchtturm Interior
- Aquarium

3. Texturen

- Progressbar.png
- Tex_Laser_Mask
- Tex_Laser_Beam

4. Shader

- Laser 
- Haze Shader 
- GlassShader


### 3rd-Party-Assets

1. Audio-Dateien

- Sämtliche Soundeffekte wurden von der Plattform Splice bezogen. Ein Teammitglied hat ein Abonnement und hat die Soundeffekte und Ambience-Sounds für das Projekt ausgewählt und heruntergeladen. [Link zu Splice](https://splice.com/)

2. 3D-Modelle

- Holzpfähle [turbosquid](https://www.turbosquid.com/3d-models/wood-3ds/957575)
- Wooden Post [sketchfab](https://sketchfab.com/3d-models/wooden-post-4991b5d72f534a339e02c10848ee322b)
- Lautsprecher [cgtrader](https://www.cgtrader.com/free-3d-models/electronics/audio/low-poly-loudspeaker-00c24fc6-a1b0-421b-86c9-7ac3abe92450)

3. Texturen

- Muschel Untergrund (SandyShell_Ground_Mat_1K) [cgtrader](https://www.cgtrader.com/free-3d-models/textures/natural-textures/ground-sandy-shell-texture)
- Dunkler Sand (ground_sand_graph_0) [adobe substance 3d assets](https://substance3d.adobe.com/community-assets/assets/ef8898c61d2be525af679fc3d239307f6a0d31dc)
- Heller Sand (ground_sand_graph_1) siehe Dunkler Sand

4. Shader
- Portal Stencil Shader [Tutorial](https://immersive-insiders.com/blog/creating-portals-using-stencil-shaders)
- Chromekey [Tutorial](https://www.youtube.com/watch?v=OEW3hUpR45s)

5. Code Packages

- DOTween Animation Library [Asset Store](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
- Helvetica 3D Font [Asset Store](https://assetstore.unity.com/packages/tools/gui/simple-helvetica-2925)
- Substance 3D for Unity [Asset Store](https://assetstore.unity.com/packages/tools/utilities/substance-3d-for-unity-213208)

## Teilsysteme und Komponenten (UML)

### High Level State Diagram des Spiels
![Game State Diagram](./Documentation/Images/GameStates.png)
Hier ist der grobe Ablauf des Spiels dargestellt. Der Spieler startet im Onboarding, in dem er die Steuerung und das Spielkonzept kennenlernt. Anschließend kann er in die Hauptwelt wechseln, in der er die Lebewesen scannen kann. Nachdem er alle Lebewesen gescannt hat, kann er den Leuchtturm betreten um ein Quiz zu absolvieren. Nachdem er das Quiz bestanden hat, ist das Spiel beendet.

### UML-Diagramm des Onboarding-Systems
![Onboarding Klassendiagramm](./Documentation/Images/Onboarding.png)
Das Onboarding-System besteht aus mehreren Komponenten, die zusammenarbeiten, um dem Spieler das Spielkonzept und die Steuerung zu vermitteln. Das Onboarding-System wird durch den OnboardingProgressController gesteuert, welcher die verschiedenen Onboarding-Phasen verwaltet. Die Onboarding-Phasen sind in als OnboardingStepSOs (ScriptableObjects) implementiert. Das Onboarding-System verwendet das Event-Bus-System, um mit anderen Komponenten zu kommunizieren. Beispielsweise wird ein Event gesendet, wenn ein OnboardingStep erfolgreich absolviert wurde.

### State Diagramm des Scanner-Systems
![Scanner State Diagram](./Documentation/Images/ScannerStates.png)

Um die Performance zu verbessern, kann der Scanner in einen "disabled" Zustand versetzt werden, in dem er keine RayCasts durchführt. Dieser Zustand wird verwendet, wenn der Spieler den Scanner nicht benutzt. Der Scanner kann ebenfalls in den "Idle" State übergehen, sobald der Spieler seinen Arm korrekt ausgerichtet hat. In diesem Zustand wird der Scanner aktiviert und kann Lebewesen scannen. Sobald ein Lebewesen gescannt wurde, wird der Scanner in den "Scanning" State versetzt, in dem der Scanner die Lebewesen scannt und ScanEvents mit dem aktuellen Fortschritt sowie des gescanntes Lebewesens sendet.

### Klassendiagramm des Teleportation Manager Systems
![Teleportation Manager Klassendiagramm](./Documentation/Images/Teleportation%20Manager.png)

Dieses Konstruk ist dafür zuständig, den Spieler von einer Szene an einen konkreten Ort einer anderen Szene und wieder zurück zu bringen. Hierzu persistieren wir (nur auf dem Hinweg), die Start- und Zielkoordinaten, sowie sowie den Auslöser der Teleportation in einem Stack. Wir speichern auch, von wo der Spieler kommt, da verschachtelte Sprünge möglich sind. Der TeleportationManager gibt dabei aus der Szene heraus die Anweisungen, speichert sie im TeleportationPlacesStack und lädt die neue Szene. Der Spieler fragt nach jedem Laden einer neuen Szene, ob er seine Position nach anggaben des TeleportationPlacesStack verändern muss und berücksichtigt dabei den Auslöser der Teleportation. Ist eine Posisitonsänderung erforderlich, setzt er diese um und löscht den entsprechenden Eintrag aus dem Stack.
