package com.unity3d.player;

import android.content.Context;
import android.graphics.Rect;
import android.graphics.SurfaceTexture;
import android.graphics.SurfaceTexture$OnFrameAvailableListener;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCaptureSession;
import android.hardware.camera2.CameraCaptureSession$CaptureCallback;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraDevice;
import android.hardware.camera2.CameraDevice$StateCallback;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.CaptureRequest;
import android.hardware.camera2.CaptureRequest$Builder;
import android.hardware.camera2.params.MeteringRectangle;
import android.hardware.camera2.params.StreamConfigurationMap;
import android.media.Image;
import android.media.ImageReader;
import android.media.ImageReader$OnImageAvailableListener;
import android.os.Handler;
import android.os.HandlerThread;
import android.util.Range;
import android.util.Size;
import android.util.SizeF;
import android.view.Surface;
import java.util.Arrays;
import java.util.concurrent.Semaphore;
import java.util.concurrent.TimeUnit;

public final class c {
    private static CameraManager b;
    private static String[] c;
    private static Semaphore e = new Semaphore(1);
    private e a;
    private CameraDevice d;
    private HandlerThread f;
    private Handler g;
    private Rect h;
    private Rect i;
    private int j;
    private int k;
    private int n;
    private int o;
    private Range q;
    private Image s;
    private CaptureRequest$Builder t;
    private int w;
    private SurfaceTexture x;
    private float l = -1.0f;
    private float m = -1.0f;
    private boolean p = false;
    private ImageReader r = null;
    private CameraCaptureSession u = null;
    private Object v = new Object();
    private Surface y = null;
    private int z = c$a.c;
    private CameraCaptureSession$CaptureCallback A = new c$1(this);
    private final CameraDevice$StateCallback B = new c$3(this);
    private final ImageReader$OnImageAvailableListener C = new c$4(this);
    private final SurfaceTexture$OnFrameAvailableListener D = new c$5(this);

    protected c(e eVar) {
        this.a = null;
        this.a = eVar;
        g();
    }

    public static int a(Context context) {
        return c(context).length;
    }

    public static int a(Context context, int i) {
        try {
            return ((Integer) b(context).getCameraCharacteristics(c(context)[i]).get(CameraCharacteristics.SENSOR_ORIENTATION)).intValue();
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
            return 0;
        }
    }

    private static int a(Range[] rangeArr, int i) {
        int i2 = -1;
        double d = Double.MAX_VALUE;
        for (int i3 = 0; i3 < rangeArr.length; i3++) {
            int iIntValue = ((Integer) rangeArr[i3].getLower()).intValue();
            int iIntValue2 = ((Integer) rangeArr[i3].getUpper()).intValue();
            float f = i;
            if (f + 0.1f > iIntValue && f - 0.1f < iIntValue2) {
                return i;
            }
            double dMin = Math.min(Math.abs(i - iIntValue), Math.abs(i - iIntValue2));
            if (dMin < d) {
                i2 = i3;
                d = dMin;
            }
        }
        return ((Integer) (i > ((Integer) rangeArr[i2].getUpper()).intValue() ? rangeArr[i2].getUpper() : rangeArr[i2].getLower())).intValue();
    }

    private static Rect a(Size[] sizeArr, double d, double d2) {
        double d3 = Double.MAX_VALUE;
        int i = 0;
        int i2 = 0;
        for (int i3 = 0; i3 < sizeArr.length; i3++) {
            int width = sizeArr[i3].getWidth();
            int height = sizeArr[i3].getHeight();
            double d4 = width;
            Double.isNaN(d4);
            double dAbs = Math.abs(Math.log(d / d4));
            double d5 = height;
            Double.isNaN(d5);
            double dAbs2 = dAbs + Math.abs(Math.log(d2 / d5));
            if (dAbs2 < d3) {
                i = width;
                i2 = height;
                d3 = dAbs2;
            }
        }
        return new Rect(0, 0, i, i2);
    }

    static CameraCaptureSession a(c cVar, CameraCaptureSession cameraCaptureSession) {
        cVar.u = cameraCaptureSession;
        return cameraCaptureSession;
    }

    static CameraDevice a(c cVar) {
        return cVar.d;
    }

    static CameraDevice a(c cVar, CameraDevice cameraDevice) {
        cVar.d = cameraDevice;
        return cameraDevice;
    }

    static CaptureRequest$Builder a(c cVar, CaptureRequest$Builder captureRequest$Builder) {
        cVar.t = captureRequest$Builder;
        return captureRequest$Builder;
    }

    static Image a(c cVar, Image image) {
        cVar.s = image;
        return image;
    }

    private void a(CameraDevice cameraDevice) {
        synchronized (this.v) {
            this.u = null;
        }
        cameraDevice.close();
        this.d = null;
    }

    static void a(c cVar, Object obj) {
        cVar.a(obj);
    }

    private void a(Object obj) {
        if (obj != "Focus") {
            if (obj == "Cancel focus") {
                synchronized (this.v) {
                    if (this.u != null) {
                        j();
                    }
                }
                return;
            }
            return;
        }
        this.p = false;
        synchronized (this.v) {
            if (this.u != null) {
                try {
                    this.t.set(CaptureRequest.CONTROL_AF_TRIGGER, 0);
                    this.t.setTag("Regular");
                    this.u.setRepeatingRequest(this.t.build(), this.A, this.g);
                } catch (CameraAccessException e2) {
                    f.Log(6, "Camera2: CameraAccessException " + e2);
                }
            }
        }
    }

    private static Size[] a(CameraCharacteristics cameraCharacteristics) {
        StreamConfigurationMap streamConfigurationMap = (StreamConfigurationMap) cameraCharacteristics.get(CameraCharacteristics.SCALER_STREAM_CONFIGURATION_MAP);
        if (streamConfigurationMap == null) {
            f.Log(6, "Camera2: configuration map is not available.");
            return null;
        }
        Size[] outputSizes = streamConfigurationMap.getOutputSizes(35);
        if (outputSizes == null || outputSizes.length == 0) {
            return null;
        }
        return outputSizes;
    }

    private static CameraManager b(Context context) {
        if (b == null) {
            b = (CameraManager) context.getSystemService("camera");
        }
        return b;
    }

    static Object b(c cVar) {
        return cVar.v;
    }

    private void b(CameraCharacteristics cameraCharacteristics) {
        int iIntValue = ((Integer) cameraCharacteristics.get(CameraCharacteristics.CONTROL_MAX_REGIONS_AF)).intValue();
        this.k = iIntValue;
        if (iIntValue > 0) {
            Rect rect = (Rect) cameraCharacteristics.get(CameraCharacteristics.SENSOR_INFO_ACTIVE_ARRAY_SIZE);
            this.i = rect;
            float fWidth = rect.width() / this.i.height();
            float fWidth2 = this.h.width() / this.h.height();
            if (fWidth2 > fWidth) {
                this.n = 0;
                this.o = (int) ((this.i.height() - (this.i.width() / fWidth2)) / 2.0f);
            } else {
                this.o = 0;
                this.n = (int) ((this.i.width() - (this.i.height() * fWidth2)) / 2.0f);
            }
            this.j = Math.min(this.i.width(), this.i.height()) / 20;
        }
    }

    static void b(c cVar, CameraDevice cameraDevice) {
        cVar.a(cameraDevice);
    }

    public static boolean b(Context context, int i) {
        try {
            return ((Integer) b(context).getCameraCharacteristics(c(context)[i]).get(CameraCharacteristics.LENS_FACING)).intValue() == 0;
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
            return false;
        }
    }

    static Surface c(c cVar) {
        return cVar.y;
    }

    public static boolean c(Context context, int i) {
        try {
            return ((Integer) b(context).getCameraCharacteristics(c(context)[i]).get(CameraCharacteristics.CONTROL_MAX_REGIONS_AF)).intValue() > 0;
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
            return false;
        }
    }

    private static String[] c(Context context) {
        if (c == null) {
            try {
                c = b(context).getCameraIdList();
            } catch (CameraAccessException e2) {
                f.Log(6, "Camera2: CameraAccessException " + e2);
                c = new String[0];
            }
        }
        return c;
    }

    public static int d(Context context, int i) {
        try {
            CameraCharacteristics cameraCharacteristics = b(context).getCameraCharacteristics(c(context)[i]);
            float[] fArr = (float[]) cameraCharacteristics.get(CameraCharacteristics.LENS_INFO_AVAILABLE_FOCAL_LENGTHS);
            SizeF sizeF = (SizeF) cameraCharacteristics.get(CameraCharacteristics.SENSOR_INFO_PHYSICAL_SIZE);
            if (fArr.length > 0) {
                return (int) ((fArr[0] * 36.0f) / sizeF.getWidth());
            }
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
        }
        return 0;
    }

    static CaptureRequest$Builder d(c cVar) {
        return cVar.t;
    }

    static ImageReader e(c cVar) {
        return cVar.r;
    }

    public static int[] e(Context context, int i) {
        try {
            Size[] sizeArrA = a(b(context).getCameraCharacteristics(c(context)[i]));
            if (sizeArrA == null) {
                return null;
            }
            int[] iArr = new int[sizeArrA.length * 2];
            for (int i2 = 0; i2 < sizeArrA.length; i2++) {
                int i3 = i2 * 2;
                iArr[i3] = sizeArrA[i2].getWidth();
                iArr[i3 + 1] = sizeArrA[i2].getHeight();
            }
            return iArr;
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
            return null;
        }
    }

    static Range f(c cVar) {
        return cVar.q;
    }

    static Semaphore f() {
        return e;
    }

    private void g() {
        HandlerThread handlerThread = new HandlerThread("CameraBackground");
        this.f = handlerThread;
        handlerThread.start();
        this.g = new Handler(this.f.getLooper());
    }

    static void g(c cVar) {
        cVar.j();
    }

    static e h(c cVar) {
        return cVar.a;
    }

    private void h() {
        this.f.quit();
        try {
            this.f.join(4000L);
            this.f = null;
            this.g = null;
        } catch (InterruptedException e2) {
            this.f.interrupt();
            f.Log(6, "Camera2: Interrupted while waiting for the background thread to finish " + e2);
        }
    }

    static Image i(c cVar) {
        return cVar.s;
    }

    private void i() {
        try {
            if (!e.tryAcquire(4L, TimeUnit.SECONDS)) {
                f.Log(5, "Camera2: Timeout waiting to lock camera for closing.");
                return;
            }
            this.d.close();
            try {
                if (!e.tryAcquire(4L, TimeUnit.SECONDS)) {
                    f.Log(5, "Camera2: Timeout waiting to close camera.");
                }
            } catch (InterruptedException e2) {
                f.Log(6, "Camera2: Interrupted while waiting to close camera " + e2);
            }
            this.d = null;
            e.release();
        } catch (InterruptedException e3) {
            f.Log(6, "Camera2: Interrupted while trying to lock camera for closing " + e3);
        }
    }

    private void j() {
        try {
            if (this.k != 0 && this.l >= 0.0f && this.l <= 1.0f && this.m >= 0.0f && this.m <= 1.0f) {
                this.p = true;
                int iWidth = (int) (((this.i.width() - (this.n * 2)) * this.l) + this.n);
                double dHeight = this.i.height() - (this.o * 2);
                double d = this.m;
                Double.isNaN(d);
                Double.isNaN(dHeight);
                double d2 = dHeight * (1.0d - d);
                double d3 = this.o;
                Double.isNaN(d3);
                this.t.set(CaptureRequest.CONTROL_AF_REGIONS, new MeteringRectangle[]{new MeteringRectangle(Math.max(this.j + 1, Math.min(iWidth, (this.i.width() - this.j) - 1)) - this.j, Math.max(this.j + 1, Math.min((int) (d2 + d3), (this.i.height() - this.j) - 1)) - this.j, this.j * 2, this.j * 2, 999)});
                this.t.set(CaptureRequest.CONTROL_AF_MODE, 1);
                this.t.set(CaptureRequest.CONTROL_AF_TRIGGER, 1);
                this.t.setTag("Focus");
                this.u.capture(this.t.build(), this.A, this.g);
                return;
            }
            this.t.set(CaptureRequest.CONTROL_AF_MODE, 4);
            this.t.setTag("Regular");
            if (this.u != null) {
                this.u.setRepeatingRequest(this.t.build(), this.A, this.g);
            }
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
        }
    }

    private void k() {
        try {
            if (this.u != null) {
                this.u.stopRepeating();
                this.t.set(CaptureRequest.CONTROL_AF_TRIGGER, 2);
                this.t.set(CaptureRequest.CONTROL_AF_MODE, 0);
                this.t.setTag("Cancel focus");
                this.u.capture(this.t.build(), this.A, this.g);
            }
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
        }
    }

    public final Rect a() {
        return this.h;
    }

    public final boolean a(float f, float f2) {
        if (this.k <= 0) {
            return false;
        }
        if (this.p) {
            f.Log(5, "Camera2: Setting manual focus point already started.");
            return false;
        }
        this.l = f;
        this.m = f2;
        synchronized (this.v) {
            if (this.u != null && this.z != c$a.b) {
                k();
            }
        }
        return true;
    }

    public final boolean a(Context context, int i, int i2, int i3, int i4, int i5) {
        try {
            CameraCharacteristics cameraCharacteristics = b.getCameraCharacteristics(c(context)[i]);
            if (((Integer) cameraCharacteristics.get(CameraCharacteristics.INFO_SUPPORTED_HARDWARE_LEVEL)).intValue() == 2) {
                f.Log(5, "Camera2: only LEGACY hardware level is supported.");
                return false;
            }
            Size[] sizeArrA = a(cameraCharacteristics);
            if (sizeArrA != null && sizeArrA.length != 0) {
                this.h = a(sizeArrA, i2, i3);
                Range[] rangeArr = (Range[]) cameraCharacteristics.get(CameraCharacteristics.CONTROL_AE_AVAILABLE_TARGET_FPS_RANGES);
                if (rangeArr == null || rangeArr.length == 0) {
                    f.Log(6, "Camera2: target FPS ranges are not avialable.");
                } else {
                    int iA = a(rangeArr, i4);
                    this.q = new Range(Integer.valueOf(iA), Integer.valueOf(iA));
                    try {
                        if (!e.tryAcquire(4L, TimeUnit.SECONDS)) {
                            f.Log(5, "Camera2: Timeout waiting to lock camera for opening.");
                            return false;
                        }
                        try {
                            b.openCamera(c(context)[i], this.B, this.g);
                            try {
                                if (!e.tryAcquire(4L, TimeUnit.SECONDS)) {
                                    f.Log(5, "Camera2: Timeout waiting to open camera.");
                                    return false;
                                }
                                e.release();
                                this.w = i5;
                                b(cameraCharacteristics);
                                return this.d != null;
                            } catch (InterruptedException e2) {
                                f.Log(6, "Camera2: Interrupted while waiting to open camera " + e2);
                            }
                        } catch (CameraAccessException e3) {
                            f.Log(6, "Camera2: CameraAccessException " + e3);
                            e.release();
                            return false;
                        }
                    } catch (InterruptedException e4) {
                        f.Log(6, "Camera2: Interrupted while trying to lock camera for opening " + e4);
                        return false;
                    }
                }
            }
            return false;
        } catch (CameraAccessException e5) {
            f.Log(6, "Camera2: CameraAccessException " + e5);
            return false;
        }
    }

    public final void b() {
        if (this.d != null) {
            e();
            i();
            this.A = null;
            this.y = null;
            this.x = null;
            Image image = this.s;
            if (image != null) {
                image.close();
                this.s = null;
            }
            ImageReader imageReader = this.r;
            if (imageReader != null) {
                imageReader.close();
                this.r = null;
            }
        }
        h();
    }

    public final void c() {
        if (this.r == null) {
            ImageReader imageReaderNewInstance = ImageReader.newInstance(this.h.width(), this.h.height(), 35, 2);
            this.r = imageReaderNewInstance;
            imageReaderNewInstance.setOnImageAvailableListener(this.C, this.g);
            this.s = null;
            if (this.w != 0) {
                SurfaceTexture surfaceTexture = new SurfaceTexture(this.w);
                this.x = surfaceTexture;
                surfaceTexture.setDefaultBufferSize(this.h.width(), this.h.height());
                this.x.setOnFrameAvailableListener(this.D, this.g);
                this.y = new Surface(this.x);
            }
        }
        try {
            if (this.u == null) {
                this.d.createCaptureSession(this.y != null ? Arrays.asList(this.y, this.r.getSurface()) : Arrays.asList(this.r.getSurface()), new c$2(this), this.g);
            } else if (this.z == c$a.b) {
                this.u.setRepeatingRequest(this.t.build(), this.A, this.g);
            }
            this.z = c$a.a;
        } catch (CameraAccessException e2) {
            f.Log(6, "Camera2: CameraAccessException " + e2);
        }
    }

    public final void d() {
        synchronized (this.v) {
            if (this.u != null) {
                try {
                    this.u.stopRepeating();
                    this.z = c$a.b;
                } catch (CameraAccessException e2) {
                    f.Log(6, "Camera2: CameraAccessException " + e2);
                }
            }
        }
    }

    public final void e() {
        synchronized (this.v) {
            if (this.u != null) {
                try {
                    this.u.abortCaptures();
                } catch (CameraAccessException e2) {
                    f.Log(6, "Camera2: CameraAccessException " + e2);
                }
                this.u.close();
                this.u = null;
                this.z = c$a.c;
            }
        }
    }
}
