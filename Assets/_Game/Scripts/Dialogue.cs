using UnityEngine;

namespace TK
{
    [System.Serializable]
    public class Dialogue
    {
        public NPC.Name name;

        [TextArea(3, 4)]
        public string[] sentences;
    }
}
