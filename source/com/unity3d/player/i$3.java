package com.unity3d.player;

import android.content.Context;
import android.view.KeyEvent;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;

final class i$3 extends EditText {
    final i a;

    i$3(i iVar, Context context) {
        super(context);
        this.a = iVar;
    }

    @Override
    public final boolean onKeyPreIme(int i, KeyEvent keyEvent) {
        if (i == 4) {
            i iVar = this.a;
            i.a(iVar, i.b(iVar), true);
            return true;
        }
        if (i == 84) {
            return true;
        }
        return super.onKeyPreIme(i, keyEvent);
    }

    @Override
    protected final void onSelectionChanged(int i, int i2) {
        i.a(this.a).reportSoftInputSelection(i, i2 - i);
    }

    @Override
    public final void onWindowFocusChanged(boolean z) {
        super.onWindowFocusChanged(z);
        if (z) {
            ((InputMethodManager) i.c(this.a).getSystemService("input_method")).showSoftInput(this, 0);
        }
    }
}
