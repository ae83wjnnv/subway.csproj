package com.unity3d.player;

import android.os.Handler$Callback;
import android.os.Looper;
import android.os.Message;

final class UnityPlayer$e$1 implements Handler$Callback {
    final UnityPlayer$e a;

    UnityPlayer$e$1(UnityPlayer$e unityPlayer$e) {
        this.a = unityPlayer$e;
    }

    private void a() {
        if (this.a.d == UnityPlayer$b.c && this.a.c) {
            UnityPlayer.access$000(this.a.i, true);
            this.a.d = UnityPlayer$b.a;
        }
    }

    @Override
    public final boolean handleMessage(Message message) {
        if (message.what != 2269) {
            return false;
        }
        UnityPlayer$d unityPlayer$d = (UnityPlayer$d) message.obj;
        if (unityPlayer$d == UnityPlayer$d.NEXT_FRAME) {
            this.a.e--;
            this.a.i.executeGLThreadJobs();
            if (!this.a.b || !this.a.c) {
                return true;
            }
            if (this.a.h >= 0) {
                if (this.a.h == 0 && UnityPlayer.access$100(this.a.i)) {
                    UnityPlayer.access$200(this.a.i);
                }
                this.a.h--;
            }
            if (!this.a.i.isFinishing() && !UnityPlayer.access$300(this.a.i)) {
                UnityPlayer.access$400(this.a.i);
            }
        } else if (unityPlayer$d == UnityPlayer$d.QUIT) {
            Looper.myLooper().quit();
        } else if (unityPlayer$d == UnityPlayer$d.RESUME) {
            this.a.b = true;
        } else if (unityPlayer$d == UnityPlayer$d.PAUSE) {
            this.a.b = false;
        } else if (unityPlayer$d == UnityPlayer$d.SURFACE_LOST) {
            this.a.c = false;
        } else {
            if (unityPlayer$d == UnityPlayer$d.SURFACE_ACQUIRED) {
                this.a.c = true;
            } else if (unityPlayer$d == UnityPlayer$d.FOCUS_LOST) {
                if (this.a.d == UnityPlayer$b.a) {
                    UnityPlayer.access$000(this.a.i, false);
                }
                this.a.d = UnityPlayer$b.b;
            } else if (unityPlayer$d == UnityPlayer$d.FOCUS_GAINED) {
                this.a.d = UnityPlayer$b.c;
            } else if (unityPlayer$d == UnityPlayer$d.URL_ACTIVATED) {
                UnityPlayer.access$500(this.a.i, this.a.i.getLaunchURL());
            } else if (unityPlayer$d == UnityPlayer$d.ORIENTATION_ANGLE_CHANGE) {
                UnityPlayer.access$600(this.a.i, this.a.f, this.a.g);
            }
            a();
        }
        if (this.a.b && this.a.e <= 0) {
            Message.obtain(this.a.a, 2269, UnityPlayer$d.NEXT_FRAME).sendToTarget();
            this.a.e++;
        }
        return true;
    }
}
