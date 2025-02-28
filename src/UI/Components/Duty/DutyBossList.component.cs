namespace KikoGuide.UI.Components.Duty;

using ImGuiNET;
using CheapLoc;
using System.Linq;
using KikoGuide.Managers;
using KikoGuide.Base;
using KikoGuide.Enums;
using System.Collections.Generic;
using System;

static class DutyBossListComponent
{
    public static void Draw(List<Duty.Boss> bosses)
    {
        try
        {
            var disabledMechanics = Service.Configuration?.hiddenMechanics;
            var shortMode = Service.Configuration?.shortenStrategies;

            foreach (var boss in bosses)
            {
                if (ImGui.CollapsingHeader(boss.Name, ImGuiTreeNodeFlags.DefaultOpen))
                {
                    if (shortMode == true && boss.TLDR != null) ImGui.TextWrapped(boss.TLDR);
                    else ImGui.TextWrapped(boss.Strategy);
                    ImGui.NewLine();

                    var keyMechanics = boss.KeyMechanics;
                    if (keyMechanics == null || keyMechanics.All(x => disabledMechanics?.Contains(x.Type) == true)) continue;

                    ImGui.BeginTable("Boss Mechanics", 3, ImGuiTableFlags.Hideable | ImGuiTableFlags.Reorderable | ImGuiTableFlags.Borders | ImGuiTableFlags.Resizable);
                    ImGui.TableSetupColumn(Loc.Localize("Generics.Mechanic", "Mechanic"), ImGuiTableColumnFlags.WidthStretch, 0.3f);
                    ImGui.TableSetupColumn(Loc.Localize("Generics.Description", "Description"), ImGuiTableColumnFlags.WidthStretch, 0.6f);
                    ImGui.TableSetupColumn(Loc.Localize("Generics.BossList.Type", "Type"), ImGuiTableColumnFlags.WidthStretch, 0.2f);
                    ImGui.TableHeadersRow();

                    foreach (var mechanic in keyMechanics)
                    {
                        if (disabledMechanics?.Contains(mechanic.Type) == true) continue;
                        ImGui.TableNextRow();
                        ImGui.TableNextColumn();
                        ImGui.Text(mechanic.Name);
                        ImGui.TableNextColumn();
                        ImGui.TextWrapped(mechanic.Description);
                        ImGui.TableNextColumn();
                        ImGui.Text(Enum.GetName(typeof(Mechanics), mechanic.Type));
                    }

                    ImGui.EndTable();
                    ImGui.NewLine();
                }
            }
        }
        catch (Exception e) { ImGui.TextColored(Colours.Error, $"Component Exception: {e.Message}"); }
    }
}


