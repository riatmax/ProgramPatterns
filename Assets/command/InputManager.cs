using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField jumpField;
    [SerializeField] private TMP_InputField shootField;

    private KeyCode jumpButton = KeyCode.W;
    private KeyCode shootButton = KeyCode.D;

    [SerializeField] private JumpCommand jc;
    [SerializeField] private ShootCommand sc;

    private void Update()
    {
        if (Input.GetKeyDown(jumpButton))
        {
            jc.execute();
        }
        if (Input.GetKeyDown(shootButton))
        {
            sc.execute();
        }
    }

    void Start()
    {
        jumpField.onEndEdit.AddListener(text => jumpButton = ParseKey(text, jumpButton, jumpField));
        shootField.onEndEdit.AddListener(text => shootButton = ParseKey(text, shootButton, shootField));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.GetContact(0).normal.y > 0.5f)
            sc.grounded = true;
    }

    KeyCode ParseKey(string text, KeyCode current, TMP_InputField field)
    {
        text = text.Trim();
        if (System.Enum.TryParse(text, true, out KeyCode key)
            && System.Enum.IsDefined(typeof(KeyCode), key)
            && !int.TryParse(text, out _))
        {
            return key;
        }

        Debug.LogWarning($"'{text}' isn't a valid key");
        field.text = current.ToString(); // revert the display
        return current;
    }
}
