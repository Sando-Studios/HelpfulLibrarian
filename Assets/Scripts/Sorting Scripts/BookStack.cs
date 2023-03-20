using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStack : MonoBehaviour
{
    [System.Serializable]
    private struct books
    {
        public BookScriptableObject bookData;
        public BookCover bookCover;
    }
    [SerializeField] private books[] booksInStack = new books[2];
    public Topics category;

    private Vector3 originalPos;
    public int speed = 300;

    private Collider2D collider2D;

    private int numOfChances = 3;
    private int numOfTries;

    public enum Location
    {
        OnCart,
        OffCart,
    };
    public Location bookLocation;

    private enum SortStatus
    {
        Sorted,
        Unsorted,
    };
    private SortStatus bookStatus;

    void OnEnable()
    {
        Bookshelf.OnSort += SortCheck;
    }
    void OnDisable()
    {
        Bookshelf.OnSort -= SortCheck;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        collider2D = GetComponent<Collider2D>();
        bookStatus = SortStatus.Unsorted;
        bookLocation = Location.OnCart;
    }

    void SortCheck(bool status, string name)
    {
        if (name == gameObject.name)
        {
            if (status) // Sorted Correctly
            {
                bookStatus = SortStatus.Sorted;
                StopCoroutine("ReturnToStartPos");
            }
            else if (!status)  // Sorted Incorrectly
            {
                bookStatus = SortStatus.Unsorted;
                TryCounter();
                StartCoroutine("ReturnToStartPos");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cart") bookLocation = Location.OnCart;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cart") bookLocation = Location.OffCart;
    }

    public void BookClicked()
    {
        if (bookLocation == Location.OnCart)
        {
            StackCover.instance.SetDescriptions(booksInStack[0].bookData.title, booksInStack[1].bookData.title,
                booksInStack[0].bookData.description, booksInStack[1].bookData.description,
                booksInStack[0].bookData.keyword.image, booksInStack[1].bookData.keyword.image);
            StackCover.instance.OpenCovers();
        }
        else if (bookLocation == Location.OffCart)
        {
            StartCoroutine("ReturnToStartPos");
        }

        if (bookStatus == SortStatus.Unsorted)
        {
            StartCoroutine("ReturnToStartPos");
        }
        else if (bookStatus == SortStatus.Sorted)
        {
            gameObject.SetActive(false);
        }
    }

    void TryCounter()
    {
        numOfTries += 1;

        if (numOfTries > numOfChances)
        {
            SetColliderStatus(false); // No longer able to interact with this stack of books
        }
    }
    // Called at the start of this part of the game
    public void SetBooksInStack(BookScriptableObject book1, BookScriptableObject book2)
    {
        booksInStack[0].bookData = book1;
        booksInStack[1].bookData = book2;
    }

    public void SetColliderStatus(bool isActive)
    {
        collider2D.enabled = isActive;
    }

    IEnumerator ReturnToStartPos()
    {
        SetColliderStatus(false);
        while (Vector2.Distance(transform.position, originalPos) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        SetColliderStatus(true);
    }
}