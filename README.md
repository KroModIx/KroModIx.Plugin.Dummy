# KroModIx.Plugin.Dummy

[![CI](https://github.com/KroModIx/KroModIx.Plugin.Dummy/actions/workflows/ci.yml/badge.svg)](https://github.com/KroModIx/KroModIx.Plugin.Dummy/actions/workflows/ci.yml)
[![Release](https://img.shields.io/github/v/release/KroModIx/KroModIx.Plugin.Dummy)](https://github.com/KroModIx/KroModIx.Plugin.Dummy/releases)

**Demo-Plugin** für den [KroModIx](https://github.com/KroModIx/KroModIx).
Zwei Tabs mit „Hello"-Content + drei Test-Aktionen (Log, Notification,
HTTP-Probe). Keine echte Mod-Verwaltung — beweist die komplette Plugin-
Kette:

**Contracts (NuGet) → Manifest-Discovery → Assembly-Load → Tab-Rendering →
Update-Notifier → REST-API-Endpoints**

## Wofür?

Zum Testen von Änderungen im Host oder im Contracts-Package ohne
komplettes Spiel-Plugin bauen zu müssen. Zeigt auch das aktuelle
Plugin-Muster für neue Plugin-Autoren (Kroste-Card-Look, Row-Bindings,
`IHostServices`-Nutzung).

## Ziel-Spiele

- Counter-Strike 2 (Steam AppId 730)
- Team Fortress 2 (Steam AppId 440)
- Proton Experimental (Steam AppId 1493710) — Dev-Fallback für Linux/Bazzite

## Installation

Aus dem [Release](https://github.com/KroModIx/KroModIx.Plugin.Dummy/releases)
das ZIP entpacken nach:

- **Windows:** `%APPDATA%\KroModIx\plugins\kroste.dummy\`
- **Linux:**   `~/.config/KroModIx/plugins/kroste.dummy/`

Alternativ: 1-Klick-Install über die Install-Karte in der KroModIx-Sidebar.

## Features

- **Hello-Tab** — zeigt Plugin-Metadata + Anzahl aktivierter Spiele
- **Info-Tab** — Detail-Kontext, Runtime-Info, Test-Buttons (Log-Line,
  Toast-Notification, HTTP-Probe gegen Public-Endpoint)
- **`IHostApiExtender`** — Plugin-eigene API-Endpoints:
  - `GET /plugins/kroste.dummy/hello` — Metadata + aktivierte Spiele
  - `GET /plugins/kroste.dummy/echo/{message}` — Echo mit Route-Parameter
- **`IUpdateNotifier`** — liefert Test-Updates pro Ziel-Spiel für die
  Kachel-Badge-Anzeige

## Entwicklung

```bash
dotnet build
```

Referenziert `KroModIx.Plugin.Contracts` aus dem GitHub-Packages-Feed
(`kromodix-github`, siehe `nuget.config`).

## Referenz

- Host: [KroModIx/KroModIx](https://github.com/KroModIx/KroModIx)
- Plugin-Skill mit Standards: [`KroModIx-Plugin`](https://github.com/KroModIx/KroModIx-Plugin)

## Lizenz

MIT — siehe [LICENSE](LICENSE).
