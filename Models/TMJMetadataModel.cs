namespace FirstGame;

public class TMJMetadataModel
{
    public int Width { get; set; }
    public int Height { get; set; }

    public int TileWidth { get; set; }
    public int TileHeight { get; set; }

    public TMJLayerModel[] Layers { get; set; }
}