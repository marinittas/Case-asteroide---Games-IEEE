using UnityEngine;

public class Meteoro : MonoBehaviour
{
    public enum Size { Grande, Pequeno }

    [Header("Tamanho")]
    public Size tamanho = Size.Grande;

    [Header("Referências")]
    public Rigidbody2D rb;
    [Tooltip("Prefab do meteoro pequeno (usado quando o grande se divide)")]
    public Meteoro pequenoPrefab;

    [Header("Movimento")]
    public float velocidadeMin = 1.5f;
    public float velocidadeMax = 3.5f;
    public float torqueMax = 45f;

    [Header("Vida")]
    public int vida = 1;

    private Camera cam;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        RandomizarMovimento(null);
    }

    public void RandomizarMovimento(Vector2? direcaoHerdada)
    {
        Vector2 dir = direcaoHerdada.HasValue && direcaoHerdada.Value.sqrMagnitude > 0.01f
            ? direcaoHerdada.Value.normalized
            : Random.insideUnitCircle.normalized;

        float velocidade = Random.Range(velocidadeMin, velocidadeMax);
        rb.linearVelocity = dir * velocidade;
        rb.angularVelocity = Random.Range(-torqueMax, torqueMax);
    }

    public void LevarDano()
    {
        if (tamanho == Size.Grande && pequenoPrefab != null)
            DividirEmDois();

        Destroy(gameObject);
    }

    private void DividirEmDois()
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("DIVIDIU: criando 2 meteoros pequenos");

            float angulo = Random.Range(20f, 160f) * (i == 0 ? 1f : -1f);
            Vector2 baseDir = rb.linearVelocity.sqrMagnitude > 0.01f ? rb.linearVelocity.normalized : Random.insideUnitCircle.normalized;
            Vector2 splitDir = (Quaternion.Euler(0, 0, angulo) * baseDir).normalized;

            Meteoro filho = Instantiate(pequenoPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            filho.RandomizarMovimento(splitDir);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // detecta colisão com objetos marcados com a tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            vida--;
            if (vida <= 0)
                LevarDano();
        }
    }

    void LateUpdate()
    {
        FazerWrap();
    }

    private void FazerWrap()
    {
        if (!cam) return;

        Vector3 vp = cam.WorldToViewportPoint(transform.position);
        bool mudou = false;

        if (vp.x < -0.05f) { vp.x = 1.05f; mudou = true; }
        else if (vp.x > 1.05f) { vp.x = -0.05f; mudou = true; }

        if (vp.y < -0.05f) { vp.y = 1.05f; mudou = true; }
        else if (vp.y > 1.05f) { vp.y = -0.05f; mudou = true; }

        if (mudou)
            transform.position = cam.ViewportToWorldPoint(vp);
    }
}
