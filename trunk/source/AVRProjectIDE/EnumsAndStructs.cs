using System;

namespace AVRProjectIDE
{
    public enum SaveResult
    {
        Successful = 1,
        Failed = 0,
        Cancelled = 2
    }

    public enum TextBoxChangeMode
    {
        Set,
        SetNewLine,
        Prepend,
        PrependNewLine,
        Append,
        AppendNewLine
    }

    public enum ListViewChangeMode
    {
        Add,
        Clear
    }

    public enum FileChangeEventType
    {
        Renamed,
        Deleted,
        Changed
    }

    public struct MemorySegment
    {
        private string type;
        public string Type
        {
            get { return type.ToLowerInvariant().Replace(' ', '_'); }
            set { type = value.ToLowerInvariant().Replace(' ', '_'); }
        }

        private string name;
        public string Name
        {
            get { return name.Replace(' ', '_'); }
            set { name = value.Replace(' ', '_'); }
        }

        private uint addr;
        public uint Addr
        {
            get { return addr; }
            set { addr = value; }
        }

        public MemorySegment(string t, string n, uint addr)
        {
            this.type = t.ToLowerInvariant().Replace(' ', '_');
            this.name = n.Replace(' ', '_');
            this.addr = addr;
        }
    }

    public enum KeywordType
    {
        Statement,
        Function,
        Type,
        Modifier,
        Constant,
        Variable,
        Preprocessor,
        Block,
        Other
    }

    public enum KeywordSource
    {
        C,
        CPP,
        Arduino,
        AVRLibc,
        User
    }

    public struct CodeKeyword
    {
        private KeywordType type;
        public KeywordType Type
        {
            get { return type; }
            set { type = value; }
        }

        private KeywordSource source;
        public KeywordSource Source
        {
            get { return source; }
            set { source = value; }
        }

        private string text;
        public string Text
        {
            get { return text.Trim(); }
            set { text = value.Trim(); }
        }

        public string ListEntry
        {
            get
            {
                string res = text;
                int i = KeywordImageGen.LookUpImgIndexForKeyword(source, type);
                if (i >= 0)
                    res += "?" + i.ToString("0");
                return res;
            }
        }

        public CodeKeyword(string text, KeywordSource source, KeywordType type)
        {
            this.text = text.Trim();
            this.source = source;
            this.type = type;
        }
    }

    public enum KeywordShape
    {
        Square,
        Box,
        Circle,
        Ring,
        Diamond,
        DiamondFrame,
        Triangle,
        TriangleFrame,
        InvertedTriangle,
        InvertedTriangleFrame,
        Hash,
        None
    }

    public struct IntVect
    {
        private string newName;
        public string NewName
        {
            get { return newName.Trim(); }
            set { newName = value.Trim(); }
        }

        private string oldName;
        public string OldName
        {
            get { return oldName.Trim(); }
            set { oldName = value.Trim(); }
        }

        private string description;
        public string Description
        {
            get { return description.Trim(); }
            set { description = value.Trim(); }
        }

        public IntVect(string newName, string oldName, string description)
        {
            this.newName = newName;
            this.oldName = oldName;
            this.description = description;
        }
    }
}