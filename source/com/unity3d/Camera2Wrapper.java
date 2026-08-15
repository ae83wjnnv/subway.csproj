package com.unity3d.player;

import android.content.Context;
import android.graphics.Rect;
import android.hardware.Camera$Area;

public class Camera2Wrapper implements e {
    private Context a;
    private c b = null;
    private final int c = 100;

    public Camera2Wrapper(Context context) {
        this.a = context;
        initCamera2Jni();
    }

    private static int a(float f) {
        return (int) Math.min(Math.max((f * 2000.0f) - 1000.0f, -900.0f), 900.0f);
    }

    private final native void deinitCamera2Jni();

    private final native void initCamera2Jni();

    private final native void nativeFrameReady(Object obj, Object obj2, Object obj3, int i, int i2, int i3);

    private final native void nativeSurfaceTextureReady(Object obj);

    public final void a() {
        deinitCamera2Jni();
        closeCamera2();
    }

    @Override
    public final void a(Object obj) {
        nativeSurfaceTextureReady(obj);
    }

    @Override
    public final void a(Object obj, Object obj2, Object obj3, int i, int i2, int i3) {
        nativeFrameReady(obj, obj2, obj3, i, i2, i3);
    }

    protected void closeCamera2() {
        c cVar = this.b;
        if (cVar != null) {
            cVar.b();
        }
        this.b = null;
    }

    protected int getCamera2Count() {
        if (PlatformSupport.LOLLIPOP_SUPPORT) {
            return c.a(this.a);
        }
        return 0;
    }

    protected int getCamera2FocalLengthEquivalent(int i) {
        if (PlatformSupport.LOLLIPOP_SUPPORT) {
            return c.d(this.a, i);
        }
        return 0;
    }

    protected int[] getCamera2Resolutions(int i) {
        if (PlatformSupport.LOLLIPOP_SUPPORT) {
            return c.e(this.a, i);
        }
        return null;
    }

    protected int getCamera2SensorOrientation(int i) {
        if (PlatformSupport.LOLLIPOP_SUPPORT) {
            return c.a(this.a, i);
        }
        return 0;
    }

    protected Object getCameraFocusArea(float f, float f2) {
        int iA = a(f);
        int iA2 = a(1.0f - f2);
        return new Camera$Area(new Rect(iA - 100, iA2 - 100, iA + 100, iA2 + 100), 1000);
    }

    protected Rect getFrameSizeCamera2() {
        c cVar = this.b;
        return cVar != null ? cVar.a() : new Rect();
    }

    protected boolean initializeCamera2(int i, int i2, int i3, int i4, int i5) {
        if (!PlatformSupport.LOLLIPOP_SUPPORT || this.b != null || UnityPlayer.currentActivity == null) {
            return false;
        }
        c cVar = new c(this);
        this.b = cVar;
        return cVar.a(this.a, i, i2, i3, i4, i5);
    }

    protected boolean isCamera2AutoFocusPointSupported(int i) {
        if (PlatformSupport.LOLLIPOP_SUPPORT) {
            return c.c(this.a, i);
        }
        return false;
    }

    protected boolean isCamera2FrontFacing(int i) {
        if (PlatformSupport.LOLLIPOP_SUPPORT) {
            return c.b(this.a, i);
        }
        return false;
    }

    protected void pauseCamera2() {
        c cVar = this.b;
        if (cVar != null) {
            cVar.d();
        }
    }

    protected boolean setAutoFocusPoint(float f, float f2) {
        c cVar;
        if (!PlatformSupport.LOLLIPOP_SUPPORT || (cVar = this.b) == null) {
            return false;
        }
        return cVar.a(f, f2);
    }

    protected void startCamera2() {
        c cVar = this.b;
        if (cVar != null) {
            cVar.c();
        }
    }

    protected void stopCamera2() {
        c cVar = this.b;
        if (cVar != null) {
            cVar.e();
        }
    }
}
