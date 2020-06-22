namespace Decadence.Engine.Checking
{
    /// <summary>
    /// Area that opens a dialogue window on entry.
    /// </summary>
    public class DialogueArea : NonDamageArea
    {
        public DialogueArea(string dialoguePath, int x, int y, int width, int height) : 
            base(Renderer.GameState.Dialogue, x, y, width, height)
        {
            OnEnter = () => XML.Load<DialogueSegment>(dialoguePath).EnterDialogue();
        }

        public DialogueArea(string dialoguePath, string modelPath, int x, int y) :
            base(Renderer.GameState.Dialogue, modelPath, x, y)
        {
            OnEnter = () => XML.Load<DialogueSegment>(dialoguePath).EnterDialogue();
        }

        public DialogueArea() { }
    }
}
