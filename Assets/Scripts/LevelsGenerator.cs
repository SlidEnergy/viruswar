using System.Collections.Generic;

public class LevelsGenerator
{
	public List<LevelInfo> Generate()
	{
		List<LevelInfo> list = new List<LevelInfo>();

		list.Add(new LevelInfo(1, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(2, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.Block, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.Block, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(3, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.Block, FigureType.None },
{ FigureType.None, FigureType.Block, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(4, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.None, FigureType.None, FigureType.Player2Virus, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(5, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.None, FigureType.None, FigureType.Player2Virus, FigureType.None },
{ FigureType.None, FigureType.Block, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.Block, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(6, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.Player2Virus, FigureType.None, FigureType.None, FigureType.Player1Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.Player2Virus },
}));
		list.Add(new LevelInfo(7, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.Player2Virus, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.Player2Virus },
}));
		list.Add(new LevelInfo(8, 4, 2, 1, 1, new FigureType[4, 4] {
{ FigureType.None, FigureType.None, FigureType.Player2Virus, FigureType.None },
{ FigureType.Block, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.Block, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.Block, FigureType.None },
}));
		list.Add(new LevelInfo(9, 5, 2, 1, 1, new FigureType[5, 5] {
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(10, 5, 2, 1, 1, new FigureType[5, 5] {
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.Block, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(11, 5, 2, 1, 1, new FigureType[5, 5] {
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
{ FigureType.None, FigureType.Block, FigureType.None, FigureType.Block, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.Block, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.Block, FigureType.None, FigureType.Block, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
}));
		list.Add(new LevelInfo(12, 5, 2, 2, 1, new FigureType[5, 5] {
{ FigureType.Player2Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.Player1Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.Player2Virus },
}));
		list.Add(new LevelInfo(13, 10, 4, 3, 3, new FigureType[10, 10] {
{ FigureType.Player2Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.Player3Virus },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None },
{ FigureType.Player1Virus, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.None, FigureType.Player4Virus },
}));

		return list;
	}
}
