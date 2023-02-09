using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using System.CodeDom.Compiler;
using Unity.VisualScripting;

#if (UNITY_EDITOR)
using UnityEditor;
using UnityEditor.UIElements;
#endif

public class BoardHolder : MonoBehaviour
{
	public Board GameArea = new Board();
	public TicTac nextPlayer = TicTac.X;
	public (bool success, TicTac nextPlayer) Play(int place)
	{
		if (GameArea[place] != TicTac.none)
			return (false, nextPlayer);
		GameArea[place] = nextPlayer;
		nextPlayer = nextPlayer.Switch();
		return (true, nextPlayer);
	}
}

[Serializable]
public class Board
{
	[HideInInspector]
	public TicTac[] BoardAsArray
	{
		get { return new TicTac[9] { TopLeft, TopCenter, TopRight, MiddleLeft, MiddleCenter, MiddleRight, BottomLeft, BottomCenter, BottomRight}; }
		set { if (value.Length != 9) throw new Board.InvalidLengthException(9, value.Length); TopLeft = value[0]; TopCenter = value[1]; TopRight = value[2]; MiddleLeft = value[3]; MiddleCenter = value[4]; MiddleRight = value[5]; BottomLeft = value[6]; BottomCenter = value[7]; BottomRight = value[8]; }
	}
	public TicTac TopLeft { get; set; }
	public TicTac TopCenter { get; set; }
	public TicTac TopRight { get; set; }
	public TicTac MiddleLeft { get; set; }
	public TicTac MiddleCenter { get; set; }
	public TicTac MiddleRight { get; set; }
	public TicTac BottomLeft { get; set; }
	public TicTac BottomCenter { get; set; }
	public TicTac BottomRight { get; set; }
	public TicTac this[int i]
	{
		get { return BoardAsArray[i]; }
		set { BoardAsArray[i] = value; }
	}

	public Board() => new Board(TicTac.none);
	public Board(TicTac? preset)
	{
		TopLeft = preset ?? TicTac.none;
		TopCenter = preset ?? TicTac.none;
		TopRight = preset ?? TicTac.none;
		MiddleLeft = preset ?? TicTac.none;
		MiddleCenter = preset ?? TicTac.none;
		MiddleRight = preset ?? TicTac.none;
		BottomLeft = preset ?? TicTac.none;
		BottomCenter = preset ?? TicTac.none;
		BottomRight = preset ?? TicTac.none;
	}
	public Board(TicTac[] boardAsArray)
	{
		TopLeft = boardAsArray[0]; TopCenter = boardAsArray[1]; TopRight = boardAsArray[2]; MiddleLeft = boardAsArray[3]; MiddleCenter = boardAsArray[4]; MiddleRight = boardAsArray[5]; BottomLeft = boardAsArray[6]; BottomCenter = boardAsArray[7]; BottomRight = boardAsArray[8];
	}
	public Board(TicTac topLeft, TicTac topCenter, TicTac topRight, TicTac middleLeft, TicTac middleCenter, TicTac middleRight, TicTac bottomLeft, TicTac bottomCenter, TicTac bottomRight)
	{
		TopLeft = topLeft;
		TopCenter = topCenter;
		TopRight = topRight;
		MiddleLeft = middleLeft;
		MiddleCenter = middleCenter;
		MiddleRight = middleRight;
		BottomLeft = bottomLeft;
		BottomCenter = bottomCenter;
		BottomRight = bottomRight;
	}
	public class InvalidLengthException : Exception
	{
		public int expectedLength { get; private set; }
		public int actualLength { get; private set; }
		public InvalidLengthException(int expected, int actual)
		{
			expectedLength = expected;
			actualLength = actual;
		}
		public override string ToString()
		{
			return $"Array of invalid length provided!\nExpected: {expectedLength}, Provided: {actualLength}";
		}
	}
}

public enum TicTac
{
	none,
	X,
	O
}

public static class TicTacExtensions
{
	public static TicTac Switch(this TicTac toe)
	{
		return toe switch
		{
			TicTac.X => TicTac.O,
			TicTac.O => TicTac.X,
			_ => TicTac.none,
		};
	}
}

/*#if (UNITY_EDITOR)
[CustomPropertyDrawer(typeof(Board))]
public class BoardEditor : PropertyDrawer
{
	//public override VisualElement CreatePropertyGUI(SerializedProperty property)
	//{
	//	VisualElement container = new VisualElement();
	//	SerializedProperty TopLeft = property.FindPropertyRelative("TopLeft");
	//	container.Add(new EnumField("TopLeft", TicTac.none));
	//	return container;
	//}
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);
		// Label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		// Rect
		Rect border = new Rect(position.x, position.y, 30, position.height);
		// Draw fields
		EditorGUI.PropertyField(border, property.FindPropertyRelative("TopLeft"), GUIContent.none);
		// Reset indent
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}
}
#endif*/