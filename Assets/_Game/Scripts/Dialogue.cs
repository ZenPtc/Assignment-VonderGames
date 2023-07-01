using UnityEngine;

namespace TK
{
    [System.Serializable]
    public class Dialogue
    {
        public NPC.Name name;

        [TextArea(3, 10)]
        public string[] sentences;
    }
}
