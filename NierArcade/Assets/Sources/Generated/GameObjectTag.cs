//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.CodeGeneratorGameObjectTag.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
[System.Serializable]
public struct GameObjectTag
{
    public GameObjectTagEnum Value;

    public override string ToString()
    {
        if (Value == GameObjectTagEnum.Tag_Untagged)
            return "Untagged";

        if (Value == GameObjectTagEnum.Tag_Respawn)
            return "Respawn";

        if (Value == GameObjectTagEnum.Tag_Finish)
            return "Finish";

        if (Value == GameObjectTagEnum.Tag_EditorOnly)
            return "EditorOnly";

        if (Value == GameObjectTagEnum.Tag_MainCamera)
            return "MainCamera";

        if (Value == GameObjectTagEnum.Tag_Player)
            return "Player";

        if (Value == GameObjectTagEnum.Tag_GameController)
            return "GameController";

        if (Value == GameObjectTagEnum.Tag_Bullet)
            return "Bullet";

        if (Value == GameObjectTagEnum.Tag_Enemy)
            return "Enemy";

        if (Value == GameObjectTagEnum.Tag_Core)
            return "Core";

        if (Value == GameObjectTagEnum.Tag_Shield)
            return "Shield";

        return "Untagged";
    }

    public static GameObjectTag Tag_Untagged
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Untagged
            };
        }
    }
    public static GameObjectTag Tag_Respawn
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Respawn
            };
        }
    }
    public static GameObjectTag Tag_Finish
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Finish
            };
        }
    }
    public static GameObjectTag Tag_EditorOnly
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_EditorOnly
            };
        }
    }
    public static GameObjectTag Tag_MainCamera
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_MainCamera
            };
        }
    }
    public static GameObjectTag Tag_Player
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Player
            };
        }
    }
    public static GameObjectTag Tag_GameController
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_GameController
            };
        }
    }
    public static GameObjectTag Tag_Bullet
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Bullet
            };
        }
    }
    public static GameObjectTag Tag_Enemy
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Enemy
            };
        }
    }
    public static GameObjectTag Tag_Core
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Core
            };
        }
    }
    public static GameObjectTag Tag_Shield
    {
        get
        {
            return new GameObjectTag
            {
                Value = GameObjectTagEnum.Tag_Shield
            };
        }
    }

    public static bool operator ==(string str, GameObjectTag tag) 
    {
        return str == tag.ToString();
    }

    public static bool operator !=(string str, GameObjectTag tag) 
    {
        return str != tag.ToString();
    }

    public static bool operator ==(GameObjectTag tag, string str) 
    {
        return tag.ToString() == str;
    }

    public static bool operator !=(GameObjectTag tag, string str) 
    {
        return tag.ToString() != str;
    }

    public static bool operator ==(GameObjectTagEnum tagEnum, GameObjectTag tag) 
    {
        return tagEnum == tag.Value;
    }

    public static bool operator !=(GameObjectTagEnum tagEnum, GameObjectTag tag) 
    {
        return tagEnum != tag.Value;
    }

    public static bool operator ==(GameObjectTag tag, GameObjectTagEnum tagEnum) 
    {
        return tag.Value == tagEnum;
    }

    public static bool operator !=(GameObjectTag tag, GameObjectTagEnum tagEnum) 
    {
        return tag.Value != tagEnum;
    }

    public static bool operator ==(GameObjectTag tag1, GameObjectTag tag2) 
    {
        return tag1.Value == tag2.Value;
    }

    public static bool operator !=(GameObjectTag tag1, GameObjectTag tag2) 
    {
        return tag1.Value != tag2.Value;
    }
}

public enum GameObjectTagEnum
{
    Tag_Untagged,
    Tag_Respawn,
    Tag_Finish,
    Tag_EditorOnly,
    Tag_MainCamera,
    Tag_Player,
    Tag_GameController,
    Tag_Bullet,
    Tag_Enemy,
    Tag_Core,
    Tag_Shield
}