using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RuleTileGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;    // RuleTile を利用して配置されるタイルマップを登録

    [SerializeField] private RuleTile ruleTile;  // 利用する RuleTile アセットを登録する
    
    [SerializeField] private Vector2Int tileOffset = new (5, 5);  // 配置位置

    [SerializeField] private int skipRate = 30;  // タイルの生成をスキップする確率
    
    [SerializeField] private int maxSkipCount = 3;  // 最大連続スキップ回数

    
    void Start() {
        // タイルの自動生成
        AutoGenerateRuleTiles();
    }

    /// <summary>
    /// タイルの自動生成
    /// </summary>
    private void AutoGenerateRuleTiles() {
        int skipCount = 0;
        
        // タイルを中央位置に配置するようにするため、原点スタートではなく、左下から初期配置するように設定
        // 代入しなくても問題なし
        //BoundsInt bounds = new BoundsInt(-tileOffset.x, -tileOffset.y, 0, tileOffset.x, tileOffset.y, 0);

        // X 軸と Y 軸のループ処理を行ってタイルを生成して配置する
        // タイルを中央位置に配置するようにするため、原点スタートではなく、左下から初期配置するように設定
        for (int x = -tileOffset.x; x < tileOffset.x; x++){
            for (int y = -tileOffset.y; y < tileOffset.y; y++) {

                // スキップ回数が上限に達していないかつスキップの確率に合致する場合、スキップ
                // たまにスキップさせる。そうすることでタイルが繋がらなくなり、ランダムな形状になる
                // if (Random.Range(0, 100) < skipRate) {
                //     skipCount++;
                //     continue;
                // }

                // タイルの配置位置の設定
                //Vector3Int tilePosition = new(x, y, 0);

                // RuleTile を利用して配置
                //tilemap.SetTile(tilePosition, ruleTile);

                //skipCount = 0;

                //Debug.Log($"タイル生成 {tilePosition}");

                
                // 下方向と左方向にタイルがないかチェック
                // bool tileBelow = tilemap.HasTile(tilePosition + new Vector3Int(0, -1, 0));
                // bool tileLeft = tilemap.HasTile(tilePosition + new Vector3Int(-1, 0, 0));
                //
                // // 下方向と左方向にタイルがない場合、タイルを配置
                // if (!tileBelow && !tileLeft) {
                //     tilemap.SetTile(tilePosition, ruleTile);
                // }
                
                
                // 4方向につながるかを確認して配置
                // if (!IsTileConnects(tilePosition)) {
                //     tilemap.SetTile(tilePosition, ruleTile);
                //     continue;
                // }
                
                // 一定の確率で新しいタイルを生成
                // if (Random.Range(0, 100) > skipRate) {
                //
                //     tilemap.SetTile(tilePosition, ruleTile);
                // }
                
                
                
                // 一定の確率で新しいタイルを生成
                if (Random.Range(0, 100) < skipRate) {
                    // 連続スキップ回数をカウント
                    skipCount++;
                }
                else
                {
                    SetRuleTile(x, y);
                    
                    //Vector3Int tilePosition = new(x, y, 0);
                    // 4方向につながるかを確認して配置
                    //if (IsTileConnects(tilePosition)) {
                        //tilemap.SetTile(tilePosition, ruleTile);
                    //    SetRuleTile(x, y);
                    //}
                }

                // 最大連続スキップ回数を超えたらタイルを配置
                if (skipCount > maxSkipCount) {
                    SetRuleTile(x, y);
                }
            }
        }

        // タイルセット
        void SetRuleTile(int x, int y) {                
            // タイルの配置位置の設定
            Vector3Int tilePosition = new(x, y, 0);

            // RuleTile を利用して配置
            tilemap.SetTile(tilePosition, ruleTile);
            
            // スキップ回数をリセット
            skipCount = 0;
        }
    }

    /// <summary>
    /// タイルが4方向につながるかを確認
    /// </summary>
    private bool IsTileConnects(Vector3Int tilePosition) {
        Vector3Int[] directions = new Vector3Int[] {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (Vector3Int direction in directions) {
            Vector3Int neighborPosition = tilePosition + direction;
            if (tilemap.HasTile(neighborPosition)) {
                return true;  // 4方向のうち少なくとも1方向につながる
            }
        }

        return false;  // 4方向すべてにつながらない
    }
}