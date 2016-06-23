<?php

namespace AllyTalks\WebApp;

use AllyTalks\Utils\Exception\SpookyException;
use AllyTalks\ORM\DoctrineFactory;
use AllyTalks\ORM\Entities\User;

class Model
{
    private $em;

    public function __construct()
    {
        $this->em = DoctrineFactory::buildEntityManagerFromConfig();
    }

    public function getAllUsers()
    {
        return $this->em->createQueryBuilder()
            ->select('u')
            ->from(User::class, 'u')
            ->getQuery()
            ->getResult();
    }

    public function addUser(array $data) : User
    {
        $user = $this->em->createQueryBuilder()
            ->select('user')
            ->from(User::class, 'user')
            ->where('user.login = :login')
            ->setParameter('login', $data['login'])
            ->getQuery()->getOneOrNullResult();

        if (!$user) {
            $user = new User($data['login'], $data['password'], $data['nickname'], $data['email']);
            $user->generateNewToken();
            $this->em->persist($user);
            $this->em->flush();

            return $user;
        } else {
            throw new SpookyException(
                '<h3>User with such login already exists!</h3> 
            <a href="/register">Return to Registration</a>'
            );
        }
    }

    public function deleteUser(User $user)
    {
        $this->em->remove($user);
        $this->em->flush();
    }

    public function getUserByLogin(string $login) : User
    {
        $user = $this->em->createQueryBuilder()
            ->select('usr')
            ->from(User::class, 'usr')
            ->where('usr.login = :log')
            ->setParameter('log', $login)
            ->getQuery()->getOneOrNullResult();

        if ($user) {
            return $user;
        } else {
            throw new \RuntimeException('No user found with current login.');
        }
    }

    public function getUserByToken(string $token) : User
    {
        $user = $this->em->createQueryBuilder()
            ->select('usr')
            ->from(User::class, 'usr')
            ->where('usr.token = :tok')
            ->setParameter('tok', $token)
            ->getQuery()->getOneOrNullResult();

        if ($user) {
            return $user;
        } else {
            throw new \RuntimeException('No user found with current token.');
        }
    }

    public function initiateFlushing()
    {
        $this->em->flush();
    }

    public function getEm()
    {
        return $this->em;
    }
}
