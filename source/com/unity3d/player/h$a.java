package com.unity3d.player;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap$Config;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.ColorDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.LayerDrawable;
import android.os.Handler;
import android.os.Looper;
import android.view.PixelCopy;
import android.view.PixelCopy$OnPixelCopyFinishedListener;
import android.view.SurfaceView;
import android.view.View;

class h$a extends View implements PixelCopy$OnPixelCopyFinishedListener {
    Bitmap a;
    final h b;

    h$a(h hVar, Context context) {
        super(context);
        this.b = hVar;
    }

    public final void a(SurfaceView surfaceView) {
        Bitmap bitmapCreateBitmap = Bitmap.createBitmap(surfaceView.getWidth(), surfaceView.getHeight(), Bitmap$Config.ARGB_8888);
        this.a = bitmapCreateBitmap;
        PixelCopy.request(surfaceView, bitmapCreateBitmap, this, new Handler(Looper.getMainLooper()));
    }

    @Override
    public final void onPixelCopyFinished(int i) {
        if (i == 0) {
            setBackground(new LayerDrawable(new Drawable[]{new ColorDrawable(-16777216), new BitmapDrawable(getResources(), this.a)}));
        }
    }
}
