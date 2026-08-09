# KroModIx.Plugin.Dummy

## Grundlagen

- **Was:** Minimal-Plugin für KroModIx. End-to-End-Proof der Plugin-Kette
  (Contracts → NuGet → Manifest → Load → UI). Kein echtes Mod-Management.
- **Stack:** .NET 10, `KroModIx.Plugin.Contracts` als PackageReference.
- **Repo:** `github.com/Kroste/KroModIx.Plugin.Dummy`.
- **Deploy-Ziel:** `~/.config/KroModIx/plugins/dummy/` (Linux) bzw.
  `%APPDATA%\KroModIx\plugins\dummy\` (Windows).

## Aktueller Stand

- v0.1.0: DummyPlugin liefert zwei Tabs („Hello" + „Info") mit Kontextinfo
  (InstallDir, UserDataDir, ProtonPrefix, PluginDataDir) und drei Test-Aktionen
  (Log-Eintrag, Notification, HTTP-Probe an example.com).
- Targets: CS2 (730), TF2 (440), Proton Experimental (1493710 — dev-Fallback).
- `plugin.json` mit `minHostVersion: "0.2.0"` — muss lokal auf `0.1.0` gedreht
  werden, solange der Host noch keinen v0.2.0-Release hat.

## Roadmap

- Nach Host-v0.2.0-Release: `minHostVersion` fest auf `0.2.0` lassen.
- Später: mehr Kontext-Anzeige, komplexere Tabs (falls für Debugging weiterer
  Plugin-Features nützlich).

## Referenz

- **NuGet-Feed:** `nuget.config` bindet `kromodix-github` (GitHub Packages) + optional
  lokalen `/tmp/local-nuget/` ein. Package-Source-Mapping ist Pflicht (CPM+multi-source
  wirft sonst NU1507).
- **Plugin-Bundle:** Release-Workflow packt `KroModIx.Plugin.Dummy.dll` +
  `plugin.json` als ZIP mit dem Namensschema `KroModIx.Plugin.Dummy-X.Y.Z.zip`.
- **`KroModIx.Plugin.Contracts`** als NuGet-Package aus GitHub Packages.
  Erst nach Host-v0.2.0-Release verfügbar; vorher lokal via `dotnet pack` in
  `/tmp/local-nuget/` und `-p:MinVerVersionOverride=0.2.0-preview`.
