using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;



namespace NiiDll
{
    public static class TypeExtention
    {
        // ================================================== 
        // クラス 
        // ================================================== 

        /// <summary> 
        /// <para>シャローコピーを生成する</para> 
        /// </summary> 
        /// <typeparam name="T">任意のクラス型</typeparam> 
        /// <param name="self">インスタンス</param> 
        /// <returns>複製したオブジェクト</returns> 
        public static T createShallowCopy<T>(this T self) where T : class
        {
            if (self == null) { return null; }

            Type selfType = self.GetType();
            var memberwiseClone = selfType.GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

            if (memberwiseClone != null)
            {
                return (T)memberwiseClone.Invoke(self, null);
            }

            return null;
        }

        /// <summary> 
        /// <para>ディープコピーを生成する</para> 
        /// </summary> 
        /// <typeparam name="T">任意のクラス型</typeparam> 
        /// <param name="self">インスタンス</param> 
        /// <returns>複製したオブジェクト</returns> 
        public static T createDeepCopy<T>(this T self) where T : class
        {
            if (self == null) { return null; }

            var copied = self.createShallowCopy(); // 値型はシャローコピーで済ませる 

            // 参照型を複製 
            var instanceType = self.GetType(); // typeof(T);//.GetElementType(); 
            var fieldInfoList = instanceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            for (int i = 0; i < fieldInfoList.Length; i++)
            {
                var fieldType = fieldInfoList[i].FieldType;

                if (fieldType.IsValueType)
                {
                    // 値型 はShallowCopyで対応済みなのでスキップ 
                    continue;
                }

                if (fieldType == typeof(string))
                {
                    // string は特殊な扱いのためスキップ 
                    continue;
                }

                if (fieldType.IsArray)
                {
                    // 配列なら中身をそれぞれコピー 
                    //object[] arrayVal = (object[])arrayObjVal; 
                    Array arrayVal = (Array)fieldInfoList[i].GetValue(copied);
                    Array copiedArrayVal = (Array)arrayVal.Clone();

                    for (int j = 0; j < arrayVal.Length; j++)
                    {
                        copiedArrayVal.SetValue(createDeepCopy(arrayVal.GetValue(j)), j);
                    }

                    fieldInfoList[i].SetValue(copied, copiedArrayVal);
                }
                else
                {
                    // 普通のコピー 
                    var copiedVal = fieldInfoList[i].GetValue(copied).createDeepCopy();
                    fieldInfoList[i].SetValue(copied, copiedVal);
                }
            }

            return copied;
        }
    }

    // 以下、動作テスト用クラス 

    public class CopyTest
    {
        public static Random _random = new Random();

        public CopyTest()
        {
            for (int i = 0; i < _ClassArrayValue.Length; i++)
            {
                _ClassArrayValue[i] = new CopyTest2();
            }
        }

        // 値型(Primitive) 
        public int _intValue = 5;

        // 値型(配列Primitive) 
        public int[] _intArrayValue = new int[] { 1, 2, 3 };

        // 値型(String) 
        public string _StringValue = "SampleText";

        // 値型(Struct) 
        public CopyTest3 _StructValue = new CopyTest3();

        // 参照型(class) 
        public CopyTest2 _ClassValue = new CopyTest2();

        // 参照型(配列class) 
        public CopyTest2[] _ClassArrayValue = new CopyTest2[4];

        public void shuffle()
        {
            _intValue = _random.Next();

            for (int i = 0; i < _intArrayValue.Length; i++)
            {
                _intArrayValue[i] = _random.Next();
            }

            _StringValue = $"{_random.Next()}";

            _StructValue.shuffle();

            _ClassValue.shuffle();

            for (int i = 0; i < _ClassArrayValue.Length; i++)
            {
                if (_ClassArrayValue[i] == null) { continue; }
                _ClassArrayValue[i].shuffle();
            }
        }
    }

    public class CopyTest2
    {
        // 値型(Primitive) 
        public int _intValue = 5;

        // 値型(配列Primitive) 
        public int[] _intArrayValue = new int[] { 1, 2, 3 };

        public void shuffle()
        {
            _intValue = CopyTest._random.Next();

            for (int i = 0; i < _intArrayValue.Length; i++)
            {
                _intArrayValue[i] = CopyTest._random.Next();
            }
        }
    }

    public struct CopyTest3
    {
        public int _F;

        public void shuffle()
        {
            _F = CopyTest._random.Next();
        }
    }
}
