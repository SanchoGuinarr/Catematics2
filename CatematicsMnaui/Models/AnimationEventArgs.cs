namespace CatematicsMnaui.Models
{
    public enum AnimationType
    {
        Correct,
        Incorrect,
    }

    public class AnimationEventArgs : EventArgs
    {
        public AnimationType AnimationType { get; set; }

        public AnimationEventArgs(AnimationType animationType)
        {
            AnimationType = animationType;
        }
    }
}