using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;

    public CharacterController controller; //motor that dirves our charaxter
    public float speed = 0.5f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVel;

    void Update()
    {
        //input sistem deðiþmiþ olabilir. 3 yýl öncekini denicem
        float horizontal = Input.GetAxisRaw("Horizontal");//sol sað a d
        float vertical = Input.GetAxisRaw("Vertical");//yukarý aþaðý w s

        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;//x y z, y axiste yukarý aþaðý gitmrsini istemiyoruz.

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }
    }

}
