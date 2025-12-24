using System;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    [SerializeField] float _raycastDistance; // Raycast Çizgisini Görmek için oluþturduk


    private Camera _camera;
    [SerializeField] Animator _anim;

    [SerializeField] ParticleSystem _ShootParticle;
    [SerializeField] AudioSource _audio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ShootParticle.Stop();

        _camera = Camera.main; // bu direkt manin camerayý çekiyor eðer main kamera deðilde herhangi bir kamera çekeceksek ve karakterin içindeyse GetComponent daha çok iþ görür
    }

    // Update is called once per frame
    void Update()
    {
        RaycastControll(); // gereksiz karmaþa olmasýn diye update içine yazmadýk bu classýn içindeki kodlarý
    }

    private void RaycastControll()
    {
        // kameranýn tam ortasýndan raycast atýyoruz normalde 0.5f demezsek tam ortadan deðil baþka yerlerden atýyor
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        // raycacstýn hitini hit deðeri ile kontrol ediyor
        RaycastHit hit;

        // kýsaca raycast deðerini raycast distance kadar atýyoruz eðer hit olursa yani bir colidera çarparsa diðer if çalýþýyor
        if (Physics.Raycast(ray,out hit ,_raycastDistance))
        {
            // raycastin deðdiði coliderdan IInteractable Scriptini arýyor varsa Diðer if çalýþýyor
            if (hit.collider.TryGetComponent(out IInteractable InteractableObject))
            {

                // tuþa basýldýðýnda ve karþýdaki obje IInteractable koduna sahipse IInteractable kodunu çalýþtýrýyor artýk o nereye baðlýysa
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    _anim.SetTrigger("Shoot");
                    _ShootParticle.Play();
                   _audio.Play();

                    InteractableObject.Interact();
                }


            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (_camera != null)
        {
            Gizmos.color = Color.green;

            // raycastin nerden baþlayýp nereye vardýðýný kontrol ediyoruz
            Vector3 _endPoint = _camera.transform.position + _camera.transform.forward * _raycastDistance;
            Gizmos.DrawLine(_camera.transform.position, _endPoint);
        }
    }
}
