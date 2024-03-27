public interface IDrag
{
    bool isMovebale { get; set; }
    bool isFreeze { get; set; }
    void onStartDrag();
    void onEndDrag();
    void onFreeze(bool isFrezeState);


}

