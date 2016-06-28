<?php

namespace AllyTalks\Utils\Token;

class TokenFactory
{
    public static function createToken(int $id) : string
    {
        return sprintf(
            '%s%s%s',
            base64_encode(openssl_random_pseudo_bytes(12)),
            base64_encode(dechex($id)),
            base64_encode(openssl_random_pseudo_bytes(12))
        );
    }

    public static function extractId(string $token) : int
    {
        return hexdec(base64_decode(substr($token, 16, -16)));
    }
}