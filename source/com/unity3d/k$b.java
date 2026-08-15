package com.unity3d.player;

import android.database.ContentObserver;
import android.os.Handler;

class k$b extends ContentObserver {
    final k a;
    private k$a b;

    public k$b(k kVar, Handler handler, k$a k_a) {
        super(handler);
        this.a = kVar;
        this.b = k_a;
    }

    @Override
    public final boolean deliverSelfNotifications() {
        return super.deliverSelfNotifications();
    }

    @Override
    public final void onChange(boolean z) {
        k$a k_a = this.b;
        if (k_a != null) {
            k_a.b();
        }
    }
}
