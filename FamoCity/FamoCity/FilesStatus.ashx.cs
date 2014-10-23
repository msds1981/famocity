using System.IO;

namespace jQueryUploadTest {
	public class FilesStatus {
		public const string HandlerPath = "/";

		public string group { get; set; }
		public string name { get; set; }
		public string FullName { get; set; }
		public string type { get; set; }
		public int size { get; set; }
		public string progress { get; set; }
		public string url { get; set; }
		public string thumbnail_url { get; set; }
		public string delete_url { get; set; }
		public string delete_type { get; set; }
		public string error { get; set; }
        public string OrignalName { get; set; }

		public FilesStatus () { }

        public FilesStatus(FileInfo fileInfo, string orignalName = "") { SetValues(fileInfo.FullName, (int)fileInfo.Length, orignalName); }

        public FilesStatus(string fileName, int fileLength, string orignalName) { SetValues(fileName, fileLength, orignalName); }

		private void SetValues (string fullname, int fileLength,string orgName) {
            FullName = fullname;

            string fileName = Path.GetFileNameWithoutExtension(fullname);
            name = fileName;
			type = "image/png";
			size = fileLength;
			progress = "1.0";
			url = HandlerPath + "FileTransferHandler.ashx?f=" + fileName;
			thumbnail_url = HandlerPath + "Thumbnail.ashx?f=" + fileName;
			delete_url = HandlerPath + "FileTransferHandler.ashx?f=" + fileName;
			delete_type = "DELETE";
            OrignalName = orgName;
		}
	}
}