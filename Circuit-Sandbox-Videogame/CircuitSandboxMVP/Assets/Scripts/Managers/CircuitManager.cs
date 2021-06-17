using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class CircuitManager : MonoBehaviour
{
    public Grid grid;
    private int index;
    public Tilemap tilemap;
    public AllSprites allSprites;
    public bool sandBoxMode;
    public bool tutorialMode;
    private HashSet<Vector3Int> placeholders = new HashSet<Vector3Int>();
    //
    private HashSet<Vector3Int> placeholderBuffers = new HashSet<Vector3Int>();
    private Vector3Int outputLocation;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public void Awake()
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase current = tilemap.GetTile(pos);
            if(current is WireTile)
            {
                WireTile wire = ScriptableObject.CreateInstance<WireTile>();
                wire.sprites = allSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), wire);
            }
            else if(current is AndTile)
            {
                AndTile and = ScriptableObject.CreateInstance<AndTile>();
                and.sprites = allSprites;
                and.sprite = allSprites.andSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), and);
            }
            else if(current is OrTile)
            {
                OrTile or = ScriptableObject.CreateInstance<OrTile>();
                or.sprites = allSprites;
                or.sprite = allSprites.orSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), or);
            }
            else if(current is NotTile)
            {
                NotTile not = ScriptableObject.CreateInstance<NotTile>();
                not.sprite = allSprites.notSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), not);
            }
            else if(current is InputOnTile)
            {
                InputOnTile inputOn = ScriptableObject.CreateInstance<InputOnTile>();
                inputOn.sprite = allSprites.inputOnSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOn);
            }
            else if(current is InputOffTile)
            {
                InputOffTile inputOff = ScriptableObject.CreateInstance<InputOffTile>();
                inputOff.sprite = allSprites.inputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOff);
            }
            else if(current is OutputTile)
            {
                OutputTile output = ScriptableObject.CreateInstance<OutputTile>();
                output.sprites = allSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), output);
                if(!sandBoxMode)
                {
                    outputLocation = new Vector3Int(pos.x, pos.y, 0);
                }
            }
            else if(current is PlaceholderTile)
            {
                PlaceholderTile placeholder = ScriptableObject.CreateInstance<PlaceholderTile>();
                placeholder.sprites = allSprites;
                placeholder.sprite = allSprites.placeholderSprite;
                placeholders.Add(new Vector3Int(pos.x, pos.y, 0));
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), placeholder);
            }
            else if(current is PlaceholderBufferTile)
            {
                PlaceholderBufferTile placeholder = ScriptableObject.CreateInstance<PlaceholderBufferTile>();
                placeholder.sprite = allSprites.placeholderBufferSprite;
                placeholderBuffers.Add(new Vector3Int(pos.x, pos.y, 0));
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), placeholder);
            }
            else if(current is BufferTile)
            {
                BufferTile buffer = ScriptableObject.CreateInstance<BufferTile>();
                buffer.sprite = allSprites.bufferSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), buffer);
            }
            else
            {
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
            }
        }
    }

    private void OnMouseDown() 
    {
        // Debug.Log("OnMouseDown()");
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);

        if(tutorialMode)
        {
            TileBase selectedTile = tilemap.GetTile(location);
            if(selectedTile is InputTile)
            {
                if(selectedTile is InputOnTile)
                {
                    InputOffTile newTile = ScriptableObject.CreateInstance<InputOffTile>();
                    newTile.sprite = allSprites.inputOffSprite;
                    tilemap.SetTile(new Vector3Int(location.x, location.y, 0), newTile);
                }
                else
                {
                    InputOnTile newTile = ScriptableObject.CreateInstance<InputOnTile>();
                    newTile.sprite = allSprites.inputOnSprite;
                    tilemap.SetTile(new Vector3Int(location.x, location.y, 0), newTile);
                }
            }
        }
        else if (sandBoxMode) {
            switch (index)
            {
                case 0: {
                    WireTile tile = ScriptableObject.CreateInstance<WireTile>();
                    tile.sprites = allSprites;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 1: {
                    AndTile tile = ScriptableObject.CreateInstance<AndTile>();
                    tile.sprites = allSprites;
                    tile.sprite = allSprites.andSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 2: {
                    OrTile tile = ScriptableObject.CreateInstance<OrTile>();
                    tile.sprites = allSprites;
                    tile.sprite = allSprites.orSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 3: {
                    NotTile tile = ScriptableObject.CreateInstance<NotTile>();
                    tile.sprite = allSprites.notSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 4: {
                    InputOffTile tile = ScriptableObject.CreateInstance<InputOffTile>();
                    tile.sprite = allSprites.inputOffSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 5: {
                    InputOnTile tile = ScriptableObject.CreateInstance<InputOnTile>();
                    tile.sprite = allSprites.inputOnSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 6: {
                    OutputTile tile = ScriptableObject.CreateInstance<OutputTile>();
                    tile.sprites = allSprites;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 7: {
                    Circuit.RemoveComponent(location);
                    tilemap.SetTile(location, null);
                    break;
                }
                case 8: {
                    BufferTile tile = ScriptableObject.CreateInstance<BufferTile>();
                    tile.sprite = allSprites.notSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                default: break;
            }            
        }
        else if (placeholders.Contains(location))
        {
            switch (index)
            {
                case 1: {
                    AndTile tile = ScriptableObject.CreateInstance<AndTile>();
                    tile.sprites = allSprites;
                    tile.sprite = allSprites.andSprite;
                    tilemap.SetTile(location, tile);

                    gameObject.GetComponent<LevelUI>().updateScore();  
                    break;
                }
                case 2: {
                    OrTile tile = ScriptableObject.CreateInstance<OrTile>();
                    tile.sprites = allSprites;
                    tile.sprite = allSprites.orSprite;
                    tilemap.SetTile(location, tile);

                    gameObject.GetComponent<LevelUI>().updateScore();
                    break;
                }
                case 7: {
                    PlaceholderTile tile = ScriptableObject.CreateInstance<PlaceholderTile>();
                    tile.sprite = allSprites.placeholderSprite;
                    tile.sprites = allSprites;
                    tilemap.SetTile(location, tile);
                    break;
                }
                default: break;
            }     
        }
        else if (placeholderBuffers.Contains(location))
        {
            switch (index)
            {
                case 3: {
                    NotTile tile = ScriptableObject.CreateInstance<NotTile>();
                    tile.sprite = allSprites.notSprite;
                    tilemap.SetTile(location, tile);

                    gameObject.GetComponent<LevelUI>().updateScore();
                    break;
                }
                case 8: {
                    BufferTile tile = ScriptableObject.CreateInstance<BufferTile>();
                    tile.sprite = allSprites.bufferSprite;
                    tilemap.SetTile(location, tile);

                    gameObject.GetComponent<LevelUI>().updateScore();
                    break;
                }
                case 7: {
                    PlaceholderBufferTile tile = ScriptableObject.CreateInstance<PlaceholderBufferTile>();
                    tile.sprite = allSprites.placeholderBufferSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                default: break;
            }     
        }
        if(!sandBoxMode && ! tutorialMode && Circuit.circuitComponents[outputLocation].on)
        {
            bool slotsCovered = true;
            foreach(Vector3Int slot in placeholders)
            {
                slotsCovered = !tilemap.GetTile<PlaceholderTile>(slot) ? slotsCovered : false;
            }
            if(slotsCovered)
            {
                victory.SetActive(true);
                screen.SetActive(true);
                infoButton.SetActive(false);
                StartCoroutine(gameObject.GetComponent<UploadToAPI>().uploadData(score));
            }
        }
    }

    public GameObject scope;
    public void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);

        scope.transform.position = grid.CellToWorld(location) + new Vector3(0.15f, 0.15f, 0);
    }

    public void WireClick() {
        index = 0;
    }

    public void AndClick() {
        index = 1;
    }

    public void OrClick() {
        index = 2;
    }

    public void NotClick() {
        index = 3;
    }

    public void OffInputClick() {
        index = 4;
    }

    public void OnInputClick() {
        index = 5;
    }

    public void OutputClick() {
        index = 6;
    }
    public void EraserClick() {
        index = 7;
    }

    public void BufferClick() {
        index = 8;
    }

    public GameObject toolBox;
    public GameObject toolBoxButton;
    public void ToolBoxToggle() {
        if (toolBox.activeSelf) {
            toolBox.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            //270
            toolBoxButton.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
        }
        else {
            toolBox.SetActive(true);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            toolBoxButton.transform.rotation = Quaternion.Euler(Vector3.forward * 0);
        }
    }

    public GameObject intructions;
    public GameObject infoButton;
    public GameObject screen;
    public GameObject victory;
    public void InstructionsToggle()
    {
        if(intructions.activeSelf)
        {
            intructions.SetActive(false);
            screen.SetActive(false);
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            infoButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            intructions.SetActive(true);
            screen.SetActive(true);
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            infoButton.GetComponent<Image>().color = Color.gray;
        }
    }
}