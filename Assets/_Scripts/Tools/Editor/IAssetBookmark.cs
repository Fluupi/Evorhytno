public interface IAssetBookmark {
    string[] BookmarkKeys { get; }

    void RunBookmarkFunction(int index);
}