package com.unity3d.player;

import android.app.Activity;
import android.app.Application$ActivityLifecycleCallbacks;
import android.content.Context;
import android.os.Bundle;
import android.view.SurfaceView;
import android.view.View;
import android.view.ViewGroup;
import java.lang.ref.WeakReference;

final class h implements Application$ActivityLifecycleCallbacks {
    Activity b;
    WeakReference a = new WeakReference(null);
    View c = null;
    h$a d = null;

    h(Context context) {
        if (context instanceof Activity) {
            Activity activity = (Activity) context;
            this.b = activity;
            activity.getApplication().registerActivityLifecycleCallbacks(this);
        }
    }

    public final void a() {
        Activity activity = this.b;
        if (activity != null) {
            activity.getApplication().unregisterActivityLifecycleCallbacks(this);
        }
    }

    public final void a(SurfaceView surfaceView) {
        if (PlatformSupport.NOUGAT_SUPPORT) {
            if (this.c == null) {
                this.d = new h$a(this, this.b);
            }
            this.d.a(surfaceView);
            this.c = this.d;
        }
    }

    public final void a(ViewGroup viewGroup) {
        View view = this.c;
        if (view == null || view.getParent() != null) {
            return;
        }
        viewGroup.addView(this.c);
        viewGroup.bringChildToFront(this.c);
    }

    public final void b(ViewGroup viewGroup) {
        View view = this.c;
        if (view == null || view.getParent() == null) {
            return;
        }
        viewGroup.removeView(this.c);
    }

    @Override
    public final void onActivityCreated(Activity activity, Bundle bundle) {
    }

    @Override
    public final void onActivityDestroyed(Activity activity) {
    }

    @Override
    public final void onActivityPaused(Activity activity) {
    }

    @Override
    public final void onActivityResumed(Activity activity) {
        this.a = new WeakReference(activity);
    }

    @Override
    public final void onActivitySaveInstanceState(Activity activity, Bundle bundle) {
    }

    @Override
    public final void onActivityStarted(Activity activity) {
    }

    @Override
    public final void onActivityStopped(Activity activity) {
    }
}
