float visibility;
float time;
sampler tex : register(s0);

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 texCoord : TEXCOORD0;
};

float PI = 3.14159265f;

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR
{
    float2 uv = input.texCoord;
    float4 col = tex2D(tex, input.texCoord);
    col.a = visibility * max(col.r, max(col.g, col.b));
    float4 glow = lerp(float4(0, 0.6, 0.2, 1), float4(0.6, 1, 1, 1), max((sin(time / 7)), 0));
    return glow * col;
}

technique SpiritRealm
{
    pass
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}


