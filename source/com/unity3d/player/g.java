package com.unity3d.player;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;

public final class g extends Fragment {
    private final IPermissionRequestCallbacks a;
    private final Activity b;
    private final Looper c;

    public g() {
        this.a = null;
        this.b = null;
        this.c = null;
    }

    public g(Activity activity, IPermissionRequestCallbacks iPermissionRequestCallbacks) {
        this.a = iPermissionRequestCallbacks;
        this.b = activity;
        this.c = Looper.myLooper();
    }

    static void a(g gVar, String[] strArr) {
        gVar.a(strArr);
    }

    private void a(String[] strArr) {
        for (String str : strArr) {
            this.a.onPermissionDenied(str);
        }
    }

    @Override
    public final void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestPermissions(getArguments().getStringArray("PermissionNames"), 96489);
    }

    @Override
    public final void onRequestPermissionsResult(int i, String[] strArr, int[] iArr) {
        if (i != 96489) {
            return;
        }
        if (strArr.length != 0) {
            for (int i2 = 0; i2 < strArr.length && i2 < iArr.length; i2++) {
                IPermissionRequestCallbacks iPermissionRequestCallbacks = this.a;
                if (iPermissionRequestCallbacks != null && this.b != null && this.c != null) {
                    if (iPermissionRequestCallbacks instanceof UnityPermissions$ModalWaitForPermissionResponse) {
                        iPermissionRequestCallbacks.onPermissionGranted(strArr[i2]);
                    } else {
                        String str = strArr[i2] == null ? "<null>" : strArr[i2];
                        new Handler(this.c).post(new g$a(this, this.a, str, iArr[i2], this.b.shouldShowRequestPermissionRationale(str)));
                    }
                }
            }
        } else if (this.a != null && this.b != null && this.c != null) {
            String[] stringArray = getArguments().getStringArray("PermissionNames");
            if (this.a instanceof UnityPermissions$ModalWaitForPermissionResponse) {
                a(stringArray);
            } else {
                new Handler(this.c).post(new g$1(this, stringArray));
            }
        }
        FragmentTransaction fragmentTransactionBeginTransaction = getActivity().getFragmentManager().beginTransaction();
        fragmentTransactionBeginTransaction.remove(this);
        fragmentTransactionBeginTransaction.commit();
    }
}
