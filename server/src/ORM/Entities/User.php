<?php

namespace AllyTalks\ORM\Entities;

use AllyTalks\Utils\Token\TokenFactory;
use Doctrine\ORM\Mapping\Column;
use Doctrine\ORM\Mapping\Entity;
use Doctrine\ORM\Mapping\Id;

/**
 * @Entity @Table(name="users")
 */
class User
{
    /** @Id @Column(type="integer") @GeneratedValue */
    protected $id;

    /** @Column(type="string") */
    protected $login;

    /** @Column(type="string") */
    protected $nickname;

    /** @Column(type="string") */
    protected $passwordHash;

    /** @Column(type="string") */
    protected $email;

    /** @Column(type="string") */
    protected $token;

    public function __construct(
        string $login,
        string $password,
        string $nickname,
        string $email
    ) {
        $this->login = $login;
        $this->email = $email;
        $this->nickname = $nickname;
        $this->passwordHash = password_hash($password, PASSWORD_BCRYPT);
        $this->generateNewToken();
    }

    public function getId() : int
    {
        return $this->id;
    }

    public function getLogin() : string
    {
        return $this->login;
    }

    public function setLogin(string $login)
    {
        $this->login = $login;
    }

    public function getEmail() : string
    {
        return $this->email;
    }

    public function setEmail(string $email)
    {
        $this->email = $email;
    }

    public function getPasswordHash() : string
    {
        return $this->passwordHash;
    }

    public function setPasswordHash(string $passwordHash)
    {
        $this->passwordHash = $passwordHash;
    }

    public function verifyPassword(string $password) : bool
    {
        return password_verify($password, $this->passwordHash);
    }

    public function getNickname() : string
    {
        return $this->nickname;
    }

    public function setNickname(string $nickname)
    {
        $this->nickname = $nickname;
    }

    public function getToken() : string
    {
        return $this->token;
    }

    public function setToken(string $token)
    {
        $this->token = $token;
    }

    public function generateNewToken()
    {
        $this->setToken(TokenFactory::createToken($this->id));
    }
}
