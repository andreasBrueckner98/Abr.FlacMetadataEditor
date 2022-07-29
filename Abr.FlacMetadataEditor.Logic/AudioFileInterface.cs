using JAudioTags;

namespace Abr.FlacMetadataEditor.Logic
{
    public class AudioFileInterface
    {
        private const string FlacExtensionUpper = "FLAC";
        private const string Mp3ExtensionUpper = "MP3";
        
        public static string? GetArtistName(string filename) => ResolveAudioFile(filename).ARTIST;

        public static string? GetTitle(string filename) => ResolveAudioFile(filename).TITLE;

        public static void ChangeInformation(string filename, string? artistName, string? title)
        {
            var audioFile = ResolveAudioFile(filename);
            audioFile.ARTIST = artistName;
            audioFile.TITLE = title;
            
            audioFile.Save(false);
        }
        
        private static AudioFile ResolveAudioFile(string filename)
        {
            AudioFile file = null;

            if (Helpers.JGetExtension(filename) == FlacExtensionUpper)
                file = new FLACFile(filename, false);
            
            if (Helpers.JGetExtension(filename) == Mp3ExtensionUpper)
                file = new MP3File(filename, false);

            return file;
        }
    }
}