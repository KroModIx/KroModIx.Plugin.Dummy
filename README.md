# ModManager.Plugins.Dummy

[![CI](https://github.com/Kroste/ModManager.Plugins.Dummy/actions/workflows/ci.yml/badge.svg)](https://github.com/Kroste/ModManager.Plugins.Dummy/actions/workflows/ci.yml)
[![Release](https://img.shields.io/github/v/release/Kroste/ModManager.Plugins.Dummy)](https://github.com/Kroste/ModManager.Plugins.Dummy/releases)

Minimal-Plugin für den [Kroste ModManager](https://github.com/Kroste/Mod-Manager).
Zeigt „Hello from Dummy" plus Kontext-Info und drei Test-Aktionen (Log, Notification,
HTTP-Probe). Keine echte Mod-Verwaltung — beweist die Plugin-Kette:

**Contracts → NuGet → Manifest-Discovery → Assembly-Load → Tab-Rendering**

## Ziel-Spiele

- Counter-Strike 2 (App-ID 730)
- Team Fortress 2 (App-ID 440)
- Proton Experimental (App-ID 1493710) — Dev-Fallback für Linux, greift auf jedem Steam-Deck/Bazzite-System

## Installation

Aus dem [Release](https://github.com/Kroste/ModManager.Plugins.Dummy/releases) das ZIP
herunterladen und entpacken nach:

- **Windows:** `%APPDATA%\ModManager\plugins\dummy\`
- **Linux:**   `~/.config/ModManager/plugins/dummy/`

Nach dem Neustart des Kroste ModManagers erscheint bei einem der Ziel-Spiele ein
goldener Stern in der Sidebar-Kachel; im Content-Bereich zeigt das Plugin zwei
Tabs („Hello", „Info").

## Entwicklung

```bash
dotnet build   # baut gegen Kroste.ModManager.PluginContracts (NuGet)
```

Das Plugin referenziert Contracts als NuGet-Package aus GitHub Packages
(`kroste-github`-Feed in `nuget.config`). Lokaler Dev-Feed `/tmp/local-nuget/`
ist konfiguriert für Tests gegen einen noch nicht gepushten Host-Build.

## Referenz

- Host: [Kroste/Mod-Manager](https://github.com/Kroste/Mod-Manager)
- Contracts: [`Kroste.ModManager.PluginContracts`](https://github.com/Kroste?tab=packages) auf GitHub Packages
- Muster für weitere Plugins: dieses Repo als Vorlage

## Lizenz

MIT — siehe [LICENSE](LICENSE).
