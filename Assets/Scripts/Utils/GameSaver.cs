using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaver
{
	[Serializable]
	public class GameData
	{
		public LevelStats[] Stats;
	}

	private string file;

	public GameSaver(string file)
	{
		if (string.IsNullOrEmpty(file))
			throw new ArgumentException("file");

		this.file = file;
	}

	public void Save(LevelStats[] stats)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		
		using (FileStream fileStream = File.Open(this.file, FileMode.OpenOrCreate))
		{
			GameData data = new GameData() { Stats = stats };

			formatter.Serialize(fileStream, data);
		}
	}

	public LevelStats[] Load()
	{
		if (File.Exists(this.file))
		{
			BinaryFormatter bf = new BinaryFormatter();

			using (FileStream file = File.Open(this.file, FileMode.Open))
			{
				GameData data = (GameData)bf.Deserialize(file);

				return data.Stats;
			}
		}

		return null;
	}
}
