package com.unity3d.player;

import android.os.Handler;
import android.os.Looper;
import com.google.android.play.core.tasks.OnSuccessListener;

class a$c implements OnSuccessListener {
    private IAssetPackManagerMobileDataConfirmationCallback a;
    private Looper b = Looper.myLooper();

    public a$c(IAssetPackManagerMobileDataConfirmationCallback iAssetPackManagerMobileDataConfirmationCallback) {
        this.a = iAssetPackManagerMobileDataConfirmationCallback;
    }

    private void a(Integer num) {
        if (this.a != null) {
            new Handler(this.b).post(new a$c$a(this.a, num.intValue() == -1));
        }
    }

    public final void onSuccess(Object obj) {
        a((Integer) obj);
    }
}
