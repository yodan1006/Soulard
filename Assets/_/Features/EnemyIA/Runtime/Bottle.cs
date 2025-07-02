using UnityEngine;

// Assurez-vous que le préfabriqué de la bouteille a bien un Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class Bottle : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject m_launcher;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Initialise la culbute de la bouteille.
    /// </summary>
    /// <param name="flightDirection">La direction dans laquelle la bouteille est lancée.</param>
    /// <param name="tumbleForce">La force de la rotation.</param>
    public void InitializeTumble(Vector3 flightDirection, float tumbleForce)
    {
        if (rb == null) return;

        // On calcule l'axe de rotation.
        // Vector3.Cross produit un vecteur perpendiculaire aux deux vecteurs en entrée.
        // En le calculant entre la direction du vol et l'axe "haut" (Vector3.up),
        // on obtient un axe de rotation parfaitement horizontal et perpendiculaire à la trajectoire.
        Vector3 rotationAxis = Vector3.Cross(flightDirection, Vector3.up);

        // On applique une impulsion de rotation unique autour de cet axe.
        // ForceMode.Impulse applique la force instantanément, comme un "coup" de poignet au lancer.
        rb.AddTorque(-rotationAxis.normalized * tumbleForce, ForceMode.Impulse);
    }

    public void SetLauncher(GameObject launcherGameObject)
    {
        m_launcher = launcherGameObject;
    }
}