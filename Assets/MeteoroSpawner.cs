using UnityEngine;

public class MeteoroSpawner : MonoBehaviour
{
    public Meteoro meteoroGrandePrefab;
    public int quantidadeInicial = 4;
    public float margemViewport = 0.5f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        for (int i = 0; i < quantidadeInicial; i++)
            Spawn();
    }

    void Spawn()
    {
        if (!cam || !meteoroGrandePrefab) return;

        Vector2 spawnPos = PontoNaBorda();
        Vector2 paraCentro = ((Vector2)cam.transform.position - spawnPos).normalized;
        Vector2 dir = (paraCentro + Random.insideUnitCircle * 0.35f).normalized;

        Meteoro m = Instantiate(meteoroGrandePrefab, spawnPos,
                                Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        m.RandomizarMovimento(dir);
    }

    Vector2 PontoNaBorda()
    {
        int lado = Random.Range(0, 4);
        Vector2 vp = Vector2.zero;

        switch (lado)
        {
            case 0: vp = new Vector2(-margemViewport, Random.value); break;
            case 1: vp = new Vector2(1f + margemViewport, Random.value); break;
            case 2: vp = new Vector2(Random.value, -margemViewport); break;
            case 3: vp = new Vector2(Random.value, 1f + margemViewport); break;
        }

        Vector3 world = cam.ViewportToWorldPoint(new Vector3(vp.x, vp.y, Mathf.Abs(cam.transform.position.z)));
        return new Vector2(world.x, world.y);
    }
}
