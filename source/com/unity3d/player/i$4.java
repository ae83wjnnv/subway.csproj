package com.unity3d.player;

import android.view.KeyEvent;
import android.widget.TextView;
import android.widget.TextView$OnEditorActionListener;

final class i$4 implements TextView$OnEditorActionListener {
    final i a;

    i$4(i iVar) {
        this.a = iVar;
    }

    @Override
    public final boolean onEditorAction(TextView textView, int i, KeyEvent keyEvent) {
        if (i == 6) {
            i iVar = this.a;
            i.a(iVar, i.b(iVar), false);
        }
        return false;
    }
}
