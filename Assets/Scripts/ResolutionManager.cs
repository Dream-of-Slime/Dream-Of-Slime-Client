using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Slime
{
    public class ResolutionManager : MonoBehaviour
    {
        [HideInInspector] public static int ResolutionX, ResolutionY;
        [SerializeField] List<BoxCollider2D> DestroyWalls;
        [SerializeField] List<BoxCollider2D> BlockWalls;
        static int Offset = 500;
        float DeltaTime = 0;
        float UpdateTime = 1;

        private void Awake()
        {
            SetResolution();
        }

        private void Update()
        {
            DeltaTime += Time.deltaTime;
            
            if (DeltaTime >= UpdateTime)
            {
                if (ResolutionX != Screen.width || ResolutionY != Screen.height)
                {
                    SetResolution();
                }
                DeltaTime = 0;
            }
        }

        public void SetResolution()
        {
            ResolutionX = Screen.width;
            ResolutionY = Screen.height;

            SetWall();
        }

        void SetWall()
        {
            DestroyWalls[0].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-Offset, ResolutionY * 0.5f, 0));
            DestroyWalls[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX * 0.5f, Offset + ResolutionY, 0));
            DestroyWalls[2].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Offset + ResolutionX, ResolutionY * 0.5f, 0));
            DestroyWalls[3].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX * 0.5f, -Offset, 0));

            DestroyWalls[0].size = new Vector2(1, ResolutionY * 0.5f);
            DestroyWalls[1].size = new Vector2(ResolutionX * 0.5f, 1);
            DestroyWalls[2].size = new Vector2(1, ResolutionY * 0.5f);
            DestroyWalls[3].size = new Vector2(ResolutionX * 0.5f, 1);

            BlockWalls[0].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, ResolutionY * 0.5f, 0)) + Vector3.right * (ResolutionX * -0.001f);
            BlockWalls[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX * 0.5f, ResolutionY, 0)) + Vector3.up * (ResolutionX * 0.001f);
            BlockWalls[2].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX, ResolutionY * 0.5f, 0)) + Vector3.right * (ResolutionX * 0.001f);
            BlockWalls[3].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX * 0.5f, 0, 0)) + Vector3.up * (ResolutionX * -0.001f);

            BlockWalls[0].size = new Vector2(ResolutionX * 0.002f, ResolutionY * 0.5f);
            BlockWalls[1].size = new Vector2(ResolutionX * 0.002f, ResolutionY * 0.5f);
            BlockWalls[2].size = new Vector2(ResolutionX * 0.002f, ResolutionY * 0.5f);
            BlockWalls[3].size = new Vector2(ResolutionX * 0.002f, ResolutionY * 0.5f);
        }
    }
}