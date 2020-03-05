using UnityEngine;
using UnityEngine.UI;

public class StructGenericTest : MonoBehaviour
{
    [SerializeField] public Text X;
    [SerializeField] public Text Y;
    [SerializeField] public Text Z;
    [SerializeField] public Text A;
    [SerializeField] public Text B;
    [SerializeField] public Text C;
    [SerializeField] public Text D;

    private void Start()
    {
        X.text = MyStruct<int>.Value.ToString();
        Y.text = MyStruct<bool>.Value.ToString();
        Z.text = MyStruct<long>.Value.ToString();
        A.text = MyStruct<uint>.Value.ToString();
        B.text = MyStruct<BaseA>.Value.ToString();
        C.text = MyStruct<InheritA>.Value.ToString();
        D.text = MyStruct<InheritInheritA>.Value.ToString();
    }
}


class BaseA
{
}

class InheritA : BaseA
{
}

class InheritInheritA : InheritA
{
}

struct MyStruct<T>
{
    public static int Value
    {
        get
        {
            if (typeof(T) == typeof(int))
            {
                return 114;
            }

            if (typeof(T) == typeof(uint))
            {
                return 514;
            }

            if (typeof(T) == typeof(long))
            {
                return 1919;
            }

            if (typeof(T) == typeof(bool))
            {
                return 810;
            }

            if (typeof(T) == typeof(BaseA))
            {
                return 33;
            }

            if (typeof(T) == typeof(InheritA))
            {
                return 4;
            }

            return -1;
        }
    }
}