namespace FirstGame;

public class TSJMetadataModel
{
    public string Image { get; set; }
    public int Columns { get; set; }
    public int TileHeight { get; set; }
    public int TileWidth { get; set; }
    public TSJTileModel[] Tiles { get; set; }
}