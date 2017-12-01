using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ParallelImagesFlip
{
    public class Startup
    {
        public static void Main()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryInfo = new DirectoryInfo(currentDirectory + "\\images");

            var files = directoryInfo.GetFiles();

            const string resultDir = "Result";

            if (!Directory.Exists(resultDir))
            {
                Directory.Delete(resultDir, true);
            }

            Directory.CreateDirectory(resultDir);

            var tasks = new List<Task>();

            foreach (var file in files)
            {
                Task.Run(() =>
                {
                    var image = Image.FromFile(file.FullName);
                    image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    image.Save($"{resultDir}\\{file.Name}");
                });
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
