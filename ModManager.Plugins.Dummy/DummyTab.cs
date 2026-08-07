using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using ModManager.PluginContracts;

namespace ModManager.Plugins.Dummy;

/// <summary>Ein Tab des Dummy-Plugins. Zeigt Kontext-Info und ein paar Beispiel-
/// Aktionen (Log-Eintrag, HTTP-Probe, Notification).</summary>
public sealed class DummyTab : IGameTabContribution
{
    private readonly DetectedGame _game;

    public DummyTab(string id, string label, string icon, int order, DetectedGame game)
    {
        Id = id;
        Label = label;
        Icon = icon;
        Order = order;
        _game = game;
    }

    public string Id { get; }
    public string Label { get; }
    public string Icon { get; }
    public int Order { get; }

    public bool IsVisible(DetectedGame game) => true;

    public Control CreateView(DetectedGame game, IHostServices host)
    {
        var title = new TextBlock
        {
            Text = $"👋  Hallo aus dem Dummy-Plugin — {game.Target.DisplayName}",
            FontSize = 18,
            FontWeight = FontWeight.SemiBold,
        };
        var installDir = new TextBlock
        {
            Text = $"Install-Dir: {game.InstallDir}",
            Opacity = 0.8,
        };
        var userDir = new TextBlock
        {
            Text = $"User-Daten:  {game.UserDataDir ?? "-"}",
            Opacity = 0.8,
        };
        var proton = new TextBlock
        {
            Text = $"Proton-Prefix: {game.ProtonPrefix ?? "-"}   ·   Runtime: {game.Runtime}",
            Opacity = 0.8,
        };
        var pluginData = new TextBlock
        {
            Text = $"Plugin-Daten:  {host.PluginDataDir}",
            Opacity = 0.8,
        };

        var logBtn = new Button { Content = "Log-Eintrag schreiben" };
        logBtn.Click += (_, _) => host.Logger.Info("Dummy-Plugin: Log-Button geklickt für {Game}", game.Target.DisplayName);

        var notifyBtn = new Button { Content = "Notification senden" };
        notifyBtn.Click += (_, _) => host.Notifications.Notify(
            $"Dummy: hallo von {game.Target.DisplayName}", NotificationLevel.Success);

        var probeBtn = new Button { Content = "HTTP-Probe an example.com" };
        probeBtn.Click += async (_, _) =>
        {
            using var scope = host.BeginProgress("HTTP-Probe");
            scope.SetIndeterminate("Requesting example.com …");
            try
            {
                using var http = host.CreateHttpClient("probe");
                var response = await http.GetAsync("https://example.com");
                host.Logger.Info("HTTP-Probe: {Status}", (int)response.StatusCode);
                host.Notifications.Notify($"HTTP {(int)response.StatusCode}", NotificationLevel.Info);
            }
            catch (Exception ex)
            {
                host.Logger.Warn(ex, "HTTP-Probe fehlgeschlagen");
                host.Notifications.Notify("HTTP-Probe fehlgeschlagen", NotificationLevel.Error);
            }
        };

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
            Children = { logBtn, notifyBtn, probeBtn },
        };

        return new ScrollViewer
        {
            Content = new StackPanel
            {
                Margin = new Thickness(24),
                Spacing = 12,
                Children = { title, installDir, userDir, proton, pluginData, buttons },
            },
        };
    }
}
