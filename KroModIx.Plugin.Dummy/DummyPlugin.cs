using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KroModIx.Plugin.Contracts;

namespace KroModIx.Plugin.Dummy;

/// <summary>
/// Minimal-Implementierung eines Game-Plugins. Kein echtes Mod-Management —
/// zeigt nur "Hello from Dummy" plus PluginDataDir und einen HTTP-Probe.
/// Ab v1.1.0 zusätzlich End-to-End-Beweis für <see cref="IHostApiExtender"/>
/// (aus KroModIx.Plugin.Contracts v1.1.0): zwei simple REST-Routen unter
/// <c>/plugins/kroste.dummy/</c>.
/// </summary>
public sealed class DummyPlugin : IGameModPlugin, IHostApiExtender
{
    public PluginMetadata Metadata { get; } = new(
        Id: "kroste.dummy",
        DisplayName: "Dummy Plugin",
        Version: "1.1.0",
        Author: "Kroste",
        Description: "Beweist die Kette Contracts → NuGet → Discovery → Load → UI + IHostApiExtender.");

    public IReadOnlyList<GameTarget> Targets { get; } = new[]
    {
        new GameTarget("counter-strike-2", "Counter-Strike 2", 730, Array.Empty<string>(), Platforms.Both),
        new GameTarget("team-fortress-2",  "Team Fortress 2",  440, Array.Empty<string>(), Platforms.Both),
        // Fallback für Bazzite/Linux-Nutzer die weder CS2 noch TF2 haben:
        // Proton Experimental hat quasi jeder Steam-Linux-Nutzer installiert.
        new GameTarget("proton-experimental", "Proton Experimental (dev-Fallback)", 1493710, Array.Empty<string>(), Platforms.Linux),
    };

    private IHostServices? _host;
    private IReadOnlyList<DetectedGame> _games = Array.Empty<DetectedGame>();

    public Task InitializeAsync(IHostServices host, IReadOnlyList<DetectedGame> activatedGames, CancellationToken ct)
    {
        _host = host;
        _games = activatedGames;
        host.Logger.Info("Dummy-Plugin initialisiert für {N} Spiel(e): {Names}",
            activatedGames.Count,
            string.Join(", ", activatedGames.Select(g => g.Target.DisplayName)));
        return Task.CompletedTask;
    }

    public IEnumerable<IGameTabContribution> GetTabContributions(DetectedGame game)
    {
        yield return new DummyTab("hello", "Hello", "\U0001F44B", 0, game);
        yield return new DummyTab("info", "Info", "ℹ️", 10, game);
    }

    public Task ShutdownAsync()
    {
        _host?.Logger.Info("Dummy-Plugin shutdown");
        return Task.CompletedTask;
    }

    // ---- IHostApiExtender (Contracts v1.1.0) ----

    public IReadOnlyList<PluginApiEndpoint> GetApiEndpoints() => new[]
    {
        new PluginApiEndpoint(
            HttpMethod: "GET",
            RelativePath: "hello",
            Summary: "Sagt hallo mit Plugin-Metadaten + Anzahl aktivierter Spiele.",
            Handler: HandleHello),
        new PluginApiEndpoint(
            HttpMethod: "GET",
            RelativePath: "echo/{message}",
            Summary: "Echo-Endpoint mit Route-Parameter — für Iteration-Loop-Smoketests.",
            Handler: HandleEcho),
    };

    private Task<PluginApiResponse> HandleHello(PluginApiRequest req, CancellationToken ct)
    {
        return Task.FromResult(PluginApiResponse.Json(new
        {
            pluginId = Metadata.Id,
            pluginVersion = Metadata.Version,
            activatedGames = _games.Count,
            gameNames = _games.Select(g => g.Target.DisplayName).ToArray(),
            hostAiAvailable = _host?.Ai is not null,
        }));
    }

    private Task<PluginApiResponse> HandleEcho(PluginApiRequest req, CancellationToken ct)
    {
        var message = req.RouteValues.TryGetValue("message", out var m) ? m : null;
        return Task.FromResult(PluginApiResponse.Json(new
        {
            echoed = message,
            query = req.Query,
            userAgent = req.Headers.TryGetValue("User-Agent", out var ua) ? ua : null,
        }));
    }
}
