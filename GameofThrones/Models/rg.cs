
public class Rootobject {
	public string Id { get; set; }
	public string Title { get; set; }
	public Recipe[] Recipes { get; set; }
}

public class Recipe {
	public int Id { get; set; }
	public string Title { get; set; }
	public string BackgroundImage { get; set; }
	public string TileImage { get; set; }
}
