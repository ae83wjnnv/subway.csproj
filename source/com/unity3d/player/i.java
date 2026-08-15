package com.unity3d.player;

import android.app.Dialog;
import android.content.Context;
import android.graphics.drawable.ColorDrawable;
import android.text.Editable;
import android.text.InputFilter;
import android.text.InputFilter$LengthFilter;
import android.text.TextWatcher;
import android.view.MotionEvent;
import android.view.View;
import android.view.View$OnClickListener;
import android.view.ViewGroup$LayoutParams;
import android.view.Window;
import android.view.WindowManager$LayoutParams;
import android.view.inputmethod.InputMethodManager;
import android.view.inputmethod.InputMethodSubtype;
import android.widget.Button;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.RelativeLayout$LayoutParams;

public final class i extends Dialog implements TextWatcher, View$OnClickListener {
    private static int d = 1627389952;
    private static int e = -1;
    public boolean a;
    private Context b;
    private UnityPlayer c;
    private int f;
    private boolean g;

    public i(Context context, UnityPlayer unityPlayer, String str, int i, boolean z, boolean z2, boolean z3, String str2, int i2, boolean z4, boolean z5) {
        super(context);
        this.b = null;
        this.c = null;
        this.b = context;
        this.c = unityPlayer;
        Window window = getWindow();
        this.a = z5;
        window.requestFeature(1);
        WindowManager$LayoutParams attributes = window.getAttributes();
        attributes.gravity = 80;
        attributes.x = 0;
        attributes.y = 0;
        window.setAttributes(attributes);
        window.setBackgroundDrawable(new ColorDrawable(0));
        View viewCreateSoftInputView = createSoftInputView();
        setContentView(viewCreateSoftInputView);
        window.setLayout(-1, -2);
        window.clearFlags(2);
        window.clearFlags(134217728);
        window.clearFlags(67108864);
        if (!this.a) {
            window.addFlags(32);
            window.addFlags(262144);
        }
        EditText editText = (EditText) findViewById(i$a.a());
        Button button = (Button) findViewById(i$a.b());
        a(editText, str, i, z, z2, z3, str2, i2);
        button.setOnClickListener(this);
        this.f = editText.getCurrentTextColor();
        a(z4);
        this.c.getViewTreeObserver().addOnGlobalLayoutListener(new i$1(this, viewCreateSoftInputView));
        editText.setOnFocusChangeListener(new i$2(this));
        editText.requestFocus();
    }

    private static int a(int i, boolean z, boolean z2, boolean z3) {
        int i2 = (z ? 32768 : 524288) | (z2 ? 131072 : 0) | (z3 ? 128 : 0);
        if (i < 0 || i > 11) {
            return i2;
        }
        int[] iArr = {1, 16385, 12290, 17, 2, 3, 8289, 33, 1, 16417, 17, 8194};
        return (iArr[i] & 2) != 0 ? iArr[i] : iArr[i] | i2;
    }

    static UnityPlayer a(i iVar) {
        return iVar.c;
    }

    private void a(EditText editText, String str, int i, boolean z, boolean z2, boolean z3, String str2, int i2) {
        editText.setImeOptions(6);
        editText.setText(str);
        editText.setHint(str2);
        editText.setHintTextColor(d);
        editText.setInputType(a(i, z, z2, z3));
        editText.setImeOptions(33554432);
        if (i2 > 0) {
            editText.setFilters(new InputFilter[]{new InputFilter$LengthFilter(i2)});
        }
        editText.addTextChangedListener(this);
        editText.setSelection(editText.getText().length());
        editText.setClickable(true);
    }

    static void a(i iVar, String str, boolean z) {
        iVar.a(str, z);
    }

    private void a(String str, boolean z) {
        ((EditText) findViewById(i$a.a())).setSelection(0, 0);
        this.c.reportSoftInputStr(str, 1, z);
    }

    private String b() {
        EditText editText = (EditText) findViewById(i$a.a());
        if (editText == null) {
            return null;
        }
        return editText.getText().toString();
    }

    static String b(i iVar) {
        return iVar.b();
    }

    static Context c(i iVar) {
        return iVar.b;
    }

    public final String a() {
        InputMethodSubtype currentInputMethodSubtype = ((InputMethodManager) this.b.getSystemService("input_method")).getCurrentInputMethodSubtype();
        if (currentInputMethodSubtype == null) {
            return null;
        }
        String locale = currentInputMethodSubtype.getLocale();
        if (locale != null && !locale.equals("")) {
            return locale;
        }
        return currentInputMethodSubtype.getMode() + " " + currentInputMethodSubtype.getExtraValue();
    }

    public final void a(int i) {
        EditText editText = (EditText) findViewById(i$a.a());
        if (editText != null) {
            if (i > 0) {
                editText.setFilters(new InputFilter[]{new InputFilter$LengthFilter(i)});
            } else {
                editText.setFilters(new InputFilter[0]);
            }
        }
    }

    public final void a(int i, int i2) {
        int i3;
        EditText editText = (EditText) findViewById(i$a.a());
        if (editText == null || editText.getText().length() < (i3 = i2 + i)) {
            return;
        }
        editText.setSelection(i, i3);
    }

    public final void a(String str) {
        EditText editText = (EditText) findViewById(i$a.a());
        if (editText != null) {
            editText.setText(str);
            editText.setSelection(str.length());
        }
    }

    public final void a(boolean z) {
        this.g = z;
        EditText editText = (EditText) findViewById(i$a.a());
        Button button = (Button) findViewById(i$a.b());
        View viewFindViewById = findViewById(i$a.c());
        if (!z) {
            editText.setBackgroundColor(e);
            editText.setTextColor(this.f);
            editText.setCursorVisible(true);
            editText.setOnClickListener(null);
            editText.setLongClickable(true);
            button.setClickable(true);
            button.setTextColor(this.f);
            viewFindViewById.setBackgroundColor(e);
            return;
        }
        editText.setBackgroundColor(0);
        editText.setTextColor(0);
        editText.setCursorVisible(false);
        editText.setOnClickListener(this);
        editText.setHighlightColor(0);
        editText.setLongClickable(false);
        button.setTextColor(0);
        viewFindViewById.setBackgroundColor(0);
        viewFindViewById.setOnClickListener(this);
    }

    @Override
    public final void afterTextChanged(Editable editable) {
        this.c.reportSoftInputStr(editable.toString(), 0, false);
    }

    @Override
    public final void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }

    protected final View createSoftInputView() {
        RelativeLayout relativeLayout = new RelativeLayout(this.b);
        relativeLayout.setLayoutParams(new ViewGroup$LayoutParams(-1, -1));
        relativeLayout.setBackgroundColor(e);
        relativeLayout.setId(i$a.c());
        i$3 i_3 = new i$3(this, this.b);
        RelativeLayout$LayoutParams relativeLayout$LayoutParams = new RelativeLayout$LayoutParams(-1, -2);
        relativeLayout$LayoutParams.addRule(15);
        relativeLayout$LayoutParams.addRule(0, i$a.b());
        i_3.setLayoutParams(relativeLayout$LayoutParams);
        i_3.setId(i$a.a());
        relativeLayout.addView(i_3);
        Button button = new Button(this.b);
        button.setText(this.b.getResources().getIdentifier("ok", "string", "android"));
        RelativeLayout$LayoutParams relativeLayout$LayoutParams2 = new RelativeLayout$LayoutParams(-2, -2);
        relativeLayout$LayoutParams2.addRule(15);
        relativeLayout$LayoutParams2.addRule(11);
        button.setLayoutParams(relativeLayout$LayoutParams2);
        button.setId(i$a.b());
        button.setBackgroundColor(0);
        relativeLayout.addView(button);
        ((EditText) relativeLayout.findViewById(i$a.a())).setOnEditorActionListener(new i$4(this));
        relativeLayout.setPadding(16, 16, 16, 16);
        return relativeLayout;
    }

    @Override
    public final boolean dispatchTouchEvent(MotionEvent motionEvent) {
        if (this.a || !(motionEvent.getAction() == 4 || this.g)) {
            return super.dispatchTouchEvent(motionEvent);
        }
        return true;
    }

    @Override
    public final void onBackPressed() {
        a(b(), true);
    }

    @Override
    public final void onClick(View view) {
        a(b(), false);
    }

    @Override
    public final void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
    }
}
