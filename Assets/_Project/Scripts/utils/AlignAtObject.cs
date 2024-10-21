using UnityEngine;
using UnityEngine.Assertions;

namespace _Project.Scripts.utils
{
    public class AlignAtObject
    {
        public static void AlignWithYourChildAt(
            Transform alignThis, Transform childOfAlignThis, Transform alignTo,
            Quaternion flip)
        {
            Assert.IsTrue(alignThis != null);
            Assert.IsTrue(childOfAlignThis != null);
            Assert.IsTrue(childOfAlignThis.IsChildOf(alignThis));
            Assert.IsTrue(alignTo != null);

            alignThis.rotation =
                alignTo.rotation *
                Quaternion.Inverse(
                    Quaternion.Inverse(alignThis.rotation) *
                    childOfAlignThis.rotation) *
                flip;

            alignThis.position =
                alignTo.position +
                (alignThis.position - childOfAlignThis.position);
        }
    }
}