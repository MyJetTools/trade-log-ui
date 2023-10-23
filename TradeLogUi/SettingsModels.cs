using SimpleTrading.SettingsReader;

[YamlAttributesOnly]
public class SettingsModel
{
    [YamlProperty("TenantFlows")]
    public string TradeLogGrpc { get; set; }
}