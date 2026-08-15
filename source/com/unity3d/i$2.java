package com.unity3d.player;

import android.view.View;
import android.view.View$OnFocusChangeListener;

final class i$2 implements View$OnFocusChangeListener {
    final i a;

    i$2(i iVar) {
        this.a = iVar;
    }

    @Override
    public final void onFocusChange(View view, boolean z) {
        if (z) {
            this.a.getWindow().setSoftInputMode(5);
        }
    }
}
