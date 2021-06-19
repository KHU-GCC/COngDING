//
//  Effect.cs
//  Congding
//
//  Created by Jeong So Yun on 2021/06/18.
//  Copyright (c) 2021 Will KHU-GCC. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{

    public float deleteTime;

    // Update is called once per frame
    void Update()
    {
        deleteTime -= Time.deltaTime;
        if(deleteTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
