using System;

public class UIEvents
{
    public static event Action OnUpdateItemAmount;
    public static void Fire_OnUpdateItemAmount() { OnUpdateItemAmount?.Invoke(); }


    public static event Action<int> OnUpdateMoney;
    public static void Fire_OnUpdateMoney(int _money) { OnUpdateMoney?.Invoke(_money); }

}
