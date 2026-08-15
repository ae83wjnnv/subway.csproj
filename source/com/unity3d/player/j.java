package com.unity3d.player;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.BitmapFactory$Options;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.ColorDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.LayerDrawable;
import android.view.View;

public final class j extends View {
    final int a;
    final int b;
    Bitmap c;
    Bitmap d;

    public j(Context context, int i) {
        super(context);
        this.a = i;
        int identifier = getResources().getIdentifier("unity_static_splash", "drawable", getContext().getPackageName());
        this.b = identifier;
        if (identifier != 0) {
            forceLayout();
        }
    }

    @Override
    public final void onDetachedFromWindow() {
        super.onDetachedFromWindow();
        Bitmap bitmap = this.c;
        if (bitmap != null) {
            bitmap.recycle();
            this.c = null;
        }
        Bitmap bitmap2 = this.d;
        if (bitmap2 != null) {
            bitmap2.recycle();
            this.d = null;
        }
    }

    @Override
    public final void onLayout(boolean z, int i, int i2, int i3, int i4) {
        if (this.b == 0) {
            return;
        }
        if (this.c == null) {
            BitmapFactory$Options bitmapFactory$Options = new BitmapFactory$Options();
            bitmapFactory$Options.inScaled = false;
            this.c = BitmapFactory.decodeResource(getResources(), this.b, bitmapFactory$Options);
        }
        int width = this.c.getWidth();
        int height = this.c.getHeight();
        int width2 = getWidth();
        int height2 = getHeight();
        if (width2 == 0 || height2 == 0) {
            return;
        }
        float f = width / height;
        float f2 = width2;
        float f3 = height2;
        boolean z2 = f2 / f3 <= f;
        int i5 = j$1.a[this.a - 1];
        if (i5 == 1) {
            if (width2 < width) {
                height = (int) (f2 / f);
                width = width2;
            }
            if (height2 < height) {
                width = (int) (f3 * f);
                height = height2;
            }
        } else if (i5 == 2 || i5 == 3) {
            if ((this.a == j$a.c) ^ z2) {
                height = (int) (f2 / f);
                width = width2;
            } else {
                width = (int) (f3 * f);
                height = height2;
            }
        }
        Bitmap bitmap = this.d;
        if (bitmap != null) {
            if (bitmap.getWidth() == width && this.d.getHeight() == height) {
                return;
            }
            Bitmap bitmap2 = this.d;
            if (bitmap2 != this.c) {
                bitmap2.recycle();
                this.d = null;
            }
        }
        Bitmap bitmapCreateScaledBitmap = Bitmap.createScaledBitmap(this.c, width, height, true);
        this.d = bitmapCreateScaledBitmap;
        bitmapCreateScaledBitmap.setDensity(getResources().getDisplayMetrics().densityDpi);
        ColorDrawable colorDrawable = new ColorDrawable(-16777216);
        BitmapDrawable bitmapDrawable = new BitmapDrawable(getResources(), this.d);
        bitmapDrawable.setGravity(17);
        setBackground(new LayerDrawable(new Drawable[]{colorDrawable, bitmapDrawable}));
    }
}
